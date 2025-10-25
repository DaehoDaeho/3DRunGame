using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") == true)
        {
            RunnerGameManager gm = FindAnyObjectByType<RunnerGameManager>();
            if (gm != null)
            {
                //gm.GameOver();
            }
        }
    }
}
