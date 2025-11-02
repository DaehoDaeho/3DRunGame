using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public FadeScreen fadeScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        fadeScreen.OnFinishedFadeOut += MoveToLobby;
    }

    private void OnDisable()
    {
        fadeScreen.OnFinishedFadeOut -= MoveToLobby;
    }

    private void Start()
    {
        fadeScreen.Fade(Fade.In);
    }

    public void OnClickScreen()
    {
        fadeScreen.Fade(Fade.Out);
    }

    void MoveToLobby()
    {
        Debug.Log("Move to Lobby");
        SceneManager.LoadScene("LobbyScene");
    }
}
