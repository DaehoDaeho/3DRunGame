using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    public float duration = 8f;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") == true)
        {
            RunnerPowerups p = other.GetComponent<RunnerPowerups>();

            if (p != null)
            {
                p.ActivateMagnet(duration);
            }

            gameObject.SetActive(false);
        }
    }
}
