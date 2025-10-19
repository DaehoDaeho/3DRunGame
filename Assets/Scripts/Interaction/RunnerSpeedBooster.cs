using UnityEngine;

public class RunnerSpeedBooster : MonoBehaviour
{
    public RunnerPlayerController playerController;
    public Camera mainCam;

    public float fovBoost = -20.0f; // 부스트 시 카메라 FOV 가산(URP 카메라의 Field of View)
    public float fovLerp = 8f;  // FOV 복귀 속도.

    private float boostTimer = 0f;
    private float currentMul = 1f;
    private float baseFov = 60f;

    private void Start()
    {
        if (playerController == null)
        {
            playerController = GetComponent<RunnerPlayerController>();
        }

        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        if (mainCam != null)
        {
            baseFov = mainCam.fieldOfView;
        }
    }

    private void Update()
    {
        if (boostTimer > 0f)
        {
            boostTimer = boostTimer - Time.deltaTime;

            if (boostTimer < 0f)
            {
                boostTimer = 0f;
            }
        }

        float targetMul = (boostTimer > 0f) ? currentMul : 1f;

        if (playerController != null)
        {
            // baseSpeed는 RunnerSpeedScaler가 올려놓은 값일 수 있음.
            // 여기서는 '곱'으로 최종 속도를 만든다.
            float baseSpeed = playerController.forwardSpeed;
            float finalSpeed = baseSpeed * targetMul;
            playerController.forwardSpeed = finalSpeed;
        }

        if (mainCam != null)
        {
            float targetFov = (boostTimer > 0f) ? (baseFov + fovBoost) : baseFov;
            mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, fovLerp * Time.deltaTime);
        }
    }

    public void TriggerBoost(float multiplier, float time)
    {
        //if (multiplier < 1f)
        //{
        //    multiplier = 1f;
        //}

        if (time <= 0f)
        {
            return;
        }

        currentMul = multiplier;
        boostTimer = time;
    }
}
