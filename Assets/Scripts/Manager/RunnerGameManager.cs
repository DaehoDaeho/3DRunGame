using UnityEngine;
using UnityEngine.SceneManagement;

public class RunnerGameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    private bool isOver = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        //Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        if(isOver == true)
        {
            return;
        }

        isOver = true;

        if(gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0.0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
