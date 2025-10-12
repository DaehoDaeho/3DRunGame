using UnityEngine;
using TMPro;

public class RunnerHUD : MonoBehaviour
{
    public Transform player;
    public TMP_Text distanceText;
    public TMP_Text speedText;
    public TMP_Text coinText;
    public Rigidbody optionalRb; // 만약 Rigidbody를 사용한다면 연결.

    private Vector3 startPos;
    private int coins = 0;

    private void Start()
    {
        if (player != null)
        {
            startPos = player.position;
        }

        UpdateAll(0f, 0f, 0);
    }

    private void Update()
    {
        float dist = 0f;

        if (player != null)
        {
            dist = player.position.z - startPos.z;

            if (dist < 0f)
            {
                dist = 0f;
            }
        }

        float speed = 0f;

        if (optionalRb != null)
        {
            speed = optionalRb.linearVelocity.magnitude;
        }
        else
        {
            speed = Mathf.Abs(Time.deltaTime) > 0f ? (RunnerSpeedCache.ZDelta / Time.deltaTime) : 0f;
        }

        UpdateAll(dist, speed, coins);
    }

    public void AddCoin(int amount)
    {
        coins = coins + amount;

        if (coins < 0)
        {
            coins = 0;
        }
    }

    private void UpdateAll(float dist, float speed, int coin)
    {
        if (distanceText != null)
        {
            distanceText.text = $"Dist: {dist:0}m";
        }

        if (speedText != null)
        {
            speedText.text = $"Speed: {speed:0.0}";
        }

        if (coinText != null)
        {
            coinText.text = $"Coins: {coin}";
        }
    }
}
