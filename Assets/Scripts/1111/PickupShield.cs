using UnityEngine;

public class PickupShield : MonoBehaviour
{
    public float duration = 8f;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") == true)
        {
            RunnerPowerups p = other.GetComponent<RunnerPowerups>();

            if (p != null)
            {
                p.ActivateShield(duration);
            }

            gameObject.SetActive(false);
        }
    }
}
