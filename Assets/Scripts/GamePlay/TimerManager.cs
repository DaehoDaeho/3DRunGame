using System.Collections;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float clearTime = 180.0f;
    public TMP_Text timerText;
    public GameObject clearPanel;
    public RunnerPlayerController playerController;

    private float currentTimer = 0.0f;

    private void Awake()
    {
        if(clearPanel != null)
        {
            clearPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if(playerController != null)
        {
            playerController.OnClearGame += ShowClearPanel;
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.OnClearGame -= ShowClearPanel;
        }
    }

    public void StartTimer()
    {
        currentTimer = clearTime;
        StartCoroutine(ProcessTimer());
    }

    IEnumerator ProcessTimer()
    {
        while (true)
        {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0.0f)
            {
                currentTimer = 0.0f;
                break;
            }

            ModifyTimer();
            yield return null;  // 다음 프레임까지 대기.
        }

        // 게임 클리어 처리.
        ModifyTimer();
    }

    public void ModifyTimer()
    {
        // 형 변환.
        int minute = (int)(currentTimer / 60);

        // 나머지 연산을 이용해서 초 단위의 값을 계산.
        int second = (int)(currentTimer % 60);

        if (timerText != null)
        {
            timerText.text = minute.ToString("D2") + ":" + second.ToString("D2");
        }

        // 타이머가 끝나면 클리어 UI 출력.
        if(currentTimer == 0.0f)
        {
            if(playerController != null)
            {
                playerController.ProcessClearGame();
            }
        }
    }

    void ShowClearPanel()
    {
        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }
    }
}
