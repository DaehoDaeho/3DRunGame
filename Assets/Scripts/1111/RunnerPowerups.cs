using UnityEngine;
using System.Collections.Generic;

public class RunnerPowerups : MonoBehaviour
{
    [Header("Shield")]
    public bool shieldActive = false;
    public float shieldDuration = 8f;

    [Header("Magnet")]
    public bool magnetActive = false;
    public float magnetDuration = 8f;
    public float magnetRadius = 4.5f;
    public float magnetPullSpeed = 10f;

    [Header("VFX (선택)")]
    public GameObject shieldFx;   // 실드 시각표시(옵션)
    public GameObject magnetFx;   // 자석 시각표시(옵션)

    private float shieldTimer = 0f;
    private float magnetTimer = 0f;

    private void Update()
    {
        // 타이머 감소.
        if (shieldActive == true)
        {
            shieldTimer = shieldTimer - Time.deltaTime;

            if (shieldTimer <= 0f)
            {
                DeactivateShield();
            }
        }

        if (magnetActive == true)
        {
            magnetTimer = magnetTimer - Time.deltaTime;

            if (magnetTimer <= 0f)
            {
                DeactivateMagnet();
            }
            else
            {
                PullCoins();
            }
        }
    }

    // --- 외부에서 호출: 파워업 발동 ---
    public void ActivateShield(float duration)
    {
        shieldActive = true;
        shieldTimer = duration;

        if (shieldFx != null)
        {
            shieldFx.SetActive(true);
        }
    }

    public void ActivateMagnet(float duration)
    {
        magnetActive = true;
        magnetTimer = duration;

        if (magnetFx != null)
        {
            magnetFx.SetActive(true);
        }
    }

    private void DeactivateShield()
    {
        shieldActive = false;
        shieldTimer = 0f;

        if (shieldFx != null)
        {
            shieldFx.SetActive(false);
        }
    }

    private void DeactivateMagnet()
    {
        magnetActive = false;
        magnetTimer = 0f;

        if (magnetFx != null)
        {
            magnetFx.SetActive(false);
        }
    }

    // 코인을 플레이어 쪽으로 끌어오기(간단: Transform 이동)
    private void PullCoins()
    {
        // 태그가 "Coin"이면 가장 간단하지만, 트리거만 사용.
        // OverlapSphere로 근처 콜라이더를 찾고 CollectCoin이 붙어 있으면 이동시킨다.
        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRadius);

        for (int i = 0; i < hits.Length; i = i + 1)
        {
            if (hits[i] == null)
            {
                continue;
            }

            CollectCoin cc = hits[i].GetComponent<CollectCoin>();

            if (cc != null && hits[i].gameObject.activeSelf == true)
            {
                Transform t = hits[i].transform;
                Vector3 toPlayer = (transform.position + new Vector3(0f, 1.0f, 0f)) - t.position;
                Vector3 step = toPlayer.normalized * magnetPullSpeed * Time.deltaTime;
                t.position = t.position + step;
            }
        }
    }

    // --- 충돌 처리 훅: 장애물을 맞았을 때 호출 ---
    // 반환값: true면 "피해가 소모되었고 진행 계속", false면 "소모 불가 -> 게임오버 처리 필요"
    public bool TryAbsorbHit()
    {
        if (shieldActive == true)
        {
            // 한 번 막고 종료.
            DeactivateShield();
            return true;
        }

        return false;
    }
}
