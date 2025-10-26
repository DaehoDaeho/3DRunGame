using UnityEngine;

public class AudioSimple : MonoBehaviour
{
    public AudioSource sfxCoin;
    public AudioSource sfxBoost;
    public AudioSource bgm;

    // 외부에서 해당 클래스 타입의 객체를 가지고 있지 않은 상태로 함수를 호출하기 위한 변수.
    public static AudioSimple Instance;

    private void Awake()
    {
        // 변수에 클래스 자신을 세팅해서 초기화.
        Instance = this;
    }

    public void PlayCoin()
    {
        Debug.Log("Play Coin Sound!!!!");
        if(sfxCoin != null)
        {
            sfxCoin.Play();
        }
    }

    public void PlayBoost()
    {
        Debug.Log("Play Boost Sound!!!!");
        if (sfxBoost != null)
        {
            sfxBoost.Play();
        }
    }

    public void StopAllSounds()
    {
        if(sfxCoin != null)
        {
            sfxCoin.Stop();
        }

        if (sfxBoost != null)
        {
            sfxBoost.Stop();
        }

        if(bgm != null)
        {
            bgm.Stop();
        }
    }
}
