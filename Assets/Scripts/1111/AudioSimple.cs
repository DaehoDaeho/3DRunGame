using UnityEngine;

public class AudioSimple : MonoBehaviour
{
    public AudioSource sfxCoin;
    public AudioSource sfxBoost;

    public static AudioSimple I;

    private void Awake()
    {
        I = this;
    }

    public void PlayCoin()
    {
        if (sfxCoin != null)
        {
            sfxCoin.Play();
        }
    }

    public void PlayBoost()
    {
        if (sfxBoost != null)
        {
            sfxBoost.Play();
        }
    }
}
