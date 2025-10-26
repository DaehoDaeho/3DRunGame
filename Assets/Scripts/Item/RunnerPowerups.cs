using UnityEngine;  // namespace
using System.Collections.Generic;

public class RunnerPowerups : MonoBehaviour
{
    public bool shieldActive = false;   // 보호막 아이템 활성화 여부.
    public float shieldDuration = 8.0f; // 보호막 지속 시간.

    public bool magnetActive = false;   // 자석 아이템 활성화 여부.
    public float magnetDuration = 8.0f; // 자석 아이템 지속 시간.
    public float magnetRadius = 5.0f;   // 자석 아이템 활성화 시 체크할 반경.
    public float magnetPullSpeed = 10.0f;   // 코인이 끌어당겨지는 속도.

    public GameObject shieldFx; // 보호막 획득 시 출력할 이펙트.
    public GameObject magnetFx; // 자석 획득 시 출력할 이펙트.

    private float shieldTimer = 0.0f;   // 보호막 획득 시 타이머를 체크하기 위한 변수.
    private float magnetTimer = 0.0f;   // 자석 획득 시 타이머를 체크하기 위한 변수.

    // Update is called once per frame
    void Update()
    {
        // 보호막이 활성화 되어 있는 중이면.
        if(shieldActive == true)
        {
            // 타이머를 계속 갱신.
            shieldTimer -= Time.deltaTime;

            // 타이머가 끝나면 보호막 효과 끄기.
            if(shieldTimer <= 0.0f)
            {
                DeactivateShield();
            }
        }

        // 자석이 활성화 되어 있는 중이면.
        if (magnetActive == true)
        {
            // 타이머를 계속 갱신.
            magnetTimer -= Time.deltaTime;

            // 타이머가 끝나면 자석 효과 끄기.
            if (magnetTimer <= 0.0f)
            {
                DeactivateMagnet();
            }
            else
            {
                // 지속 시간이 아직 남아 있을 경우 주변의 코인을 체크해서 끌어당기는 처리를 한다.
                PullCoin();
            }
        }
    }

    /// <summary>
    /// 보호막 효과를 활성화.
    /// </summary>
    /// <param name="duration">지속 시간.</param>
    public void ActivateShield(float duration)
    {
        shieldActive = true;    // 보호막 효과 활성화.
        shieldTimer = duration; // 타이머 세팅.

        // 이펙트가 있을 경우 출력.
        if(shieldFx != null)
        {
            shieldFx.SetActive(true);
        }
    }

    /// <summary>
    /// 보호막 효과 비활성화.
    /// </summary>
    private void DeactivateShield()
    {
        shieldActive = false;   // 보호막 효과 비활성화.
        shieldTimer = 0.0f; // 타이머 초기화.

        // 이펙트가 있을 경우 끄기.
        if (shieldFx != null)
        {
            shieldFx.SetActive(false);
        }
    }

    /// <summary>
    /// 자석 효과를 활성화.
    /// </summary>
    /// <param name="duration"></param>
    public void ActivateMagnet(float duration)
    {
        magnetActive = true;    // 자석 효과 활성화.
        magnetTimer = duration; // 타이머 세팅.

        // 이펙트가 있을 경우 출력.
        if (magnetFx != null)
        {
            magnetFx.SetActive(true);
        }
    }

    /// <summary>
    /// 자석 효과 비활성화.
    /// </summary>
    private void DeactivateMagnet()
    {
        magnetActive = false;   // 자석 효과 비활성화.
        magnetTimer = 0.0f; // 타이머 초기화.

        // 이펙트가 있을 경우 끄기.
        if (magnetFx != null)
        {
            magnetFx.SetActive(false);
        }
    }

    /// <summary>
    /// 코인을 플레이어 쪽으로 끌어오기.
    /// </summary>
    private void PullCoin()
    {
        // 지정된 위치에서 지정된 반경만큼의 원을 생성해서 그 원 안에 들어간 오브젝트 정보를 가져온다.
        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRadius);

        //  위에서 감지된 오브젝트의 수만큼 반복문을 돌며 처리.
        for(int i=0; i<hits.Length; ++i)
        {
            // 만약 오브젝트의 정보가 유효하지 않다면 continue로 처리 생략.
            if (hits[i] == null)
            {
                continue;
            }

            CollectCoin cc = hits[i].GetComponent<CollectCoin>();

            // 감지된 오브젝트가 코인이고, 현재 활성화 되어 있을 경우 처리.
            if(cc != null && hits[i].gameObject.activeSelf == true)
            {
                Transform t = hits[i].transform;    // 코인의 현재 트랜스폼 정보를 가져온다.

                // 플레이어의 방향을 향해서 이동해야 할 이동량을 계산한다.
                Vector3 toPlayer = (transform.position + new Vector3(0.0f, 1.0f, 0.0f)) - t.position;
                Vector3 step = toPlayer.normalized * magnetPullSpeed * Time.deltaTime;

                // 코인의 위치 정보를 갱신한다.
                t.position = t.position + step;
            }
        }
    }

    /// <summary>
    /// 보호막 효과를 외부에서 비활성화.
    /// </summary>
    /// <returns></returns>
    public bool TryAbsorbHit()
    {
        // 현재 보호막 효과가 활성화 중일 경우 비활성화 처리.
        if(shieldActive == true)
        {
            DeactivateShield();
            return true;
        }

        return false;
    }
}
