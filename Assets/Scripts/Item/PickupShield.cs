using UnityEngine;

public class PickupShield : MonoBehaviour
{
    public float duration = 8.0f;   // 지속 시간.

    /// <summary>
    /// 오브젝트끼리 트리거 충돌 시 자동으로 호출.
    /// </summary>
    /// <param name="other">충돌한 오브젝트.</param>
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트의 태그를 체크해서 플레이어인지 확인.
        if(other.CompareTag("Player") == true)
        {
            // 플레이어 오브젝트의 RunnerPowerups 컴포넌트를 가져온다.
            RunnerPowerups p = other.GetComponent<RunnerPowerups>();

            // 보호막 효과 활성화.
            if (p != null)
            {
                p.ActivateShield(duration);
            }

            // 보호막 오브젝트 비활성화.
            gameObject.SetActive(false);
        }
    }
}
