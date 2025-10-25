using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") == true)
        {
            RunnerCheckpointManager cpm = other.GetComponent<RunnerCheckpointManager>();

            if (cpm != null)
            {
                cpm.SetCheckpoint(transform.position.z);
            }

            // 시각 피드백: 비활성 또는 색 변경.
            gameObject.SetActive(false);
        }
    }
}
