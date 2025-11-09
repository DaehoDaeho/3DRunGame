using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    public FadeScreen fadeScreen;

    private void OnEnable()
    {
        fadeScreen.OnFinishedFadeOut += MoveToGame;
    }

    private void OnDisable()
    {
        fadeScreen.OnFinishedFadeOut -= MoveToGame;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1.0f;
        fadeScreen.Fade(Fade.In);    
    }

    public void OnClickStart()
    {
        fadeScreen.Fade(Fade.Out);
    }

    void MoveToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
