using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameScene : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public TMP_Text countText;
    public TimerManager timerManager;

    private void OnEnable()
    {
        fadeScreen.OnFinishedFadeOut += ReturnToLobby;
    }

    private void OnDisable()
    {
        fadeScreen.OnFinishedFadeOut -= ReturnToLobby;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0.0f;
        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        if(countText == null)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            countText.gameObject.SetActive(true);
            countText.text = "3";
            yield return new WaitForSecondsRealtime(1.0f);
            countText.text = "2";
            yield return new WaitForSecondsRealtime(1.0f);
            countText.text = "1";
            yield return new WaitForSecondsRealtime(1.0f);
            countText.gameObject.SetActive(false);
            Time.timeScale = 1.0f;

            if(timerManager != null)
            {
                timerManager.StartTimer();
            }
        }
    }

    public void OnClickReturnToLobby()
    {
        fadeScreen.Fade(Fade.Out);
    }

    void ReturnToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
