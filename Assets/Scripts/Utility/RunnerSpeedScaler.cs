using UnityEngine;

public class RunnerSpeedScaler : MonoBehaviour
{
    public RunnerPlayerController playerController;

    public float startSpeed = 8f;
    public float maxSpeed = 16f;
    public float rampTime = 120f; // 이 시간 동안 start -> max로 선형 증가.

    private float t = 0f;

    private void Start()
    {
        if (playerController != null)
        {
            playerController.forwardSpeed = startSpeed;
        }
    }

    private void Update()
    {
        if (playerController == null)
        {
            return;
        }

        t = t + Time.deltaTime;

        if (t > rampTime)
        {
            t = rampTime;
        }

        float k = (rampTime > 0f) ? (t / rampTime) : 1f;

        if (k < 0f)
        {
            k = 0f;
        }

        if (k > 1f)
        {
            k = 1f;
        }

        float s = Mathf.Lerp(startSpeed, maxSpeed, k);
        playerController.forwardSpeed = s;
    }
}
