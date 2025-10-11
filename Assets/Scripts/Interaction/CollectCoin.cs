using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") == true)
        {
            RunnerHUD hud = FindAnyObjectByType<RunnerHUD>();

            if (hud != null)
            {
                hud.AddCoin(amount);
            }

            gameObject.SetActive(false);
        }
    }
}
