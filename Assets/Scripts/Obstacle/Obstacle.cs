using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            // 플레이어가 보호막을 획득한 상태인지 체크해서 획득한 상태면 사망 처리를 하지 않는다.
            RunnerPowerups p = other.GetComponent<RunnerPowerups>();
            if(p != null)
            {
                bool absourbed = p.TryAbsorbHit();
                if (absourbed == true)
                {
                    return;
                }
            }

            RunnerPlayerController pc = other.GetComponent<RunnerPlayerController>();
            if(pc != null)
            {
                pc.TakeDamage(1);
            }
        }
    }
}
