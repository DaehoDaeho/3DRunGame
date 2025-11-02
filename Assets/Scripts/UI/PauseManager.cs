using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    private void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        //Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.R) == true)
        {
            if (isPaused == true)
            {
                // 일시정지 중 R을 눌러도 재시작이 동작하도록 허용.
                RestartScene();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }

        Time.timeScale = isPaused ? 0.0f : 1.0f;
    }

    public void RestartScene()
    {
        // 간단 재시작(현재 씬 다시 로드)
        // using UnityEngine.SceneManagement; 필요.
        UnityEngine.SceneManagement.Scene s = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        UnityEngine.SceneManagement.SceneManager.LoadScene(s.name);
    }
}
