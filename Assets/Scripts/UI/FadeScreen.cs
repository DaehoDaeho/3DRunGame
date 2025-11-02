using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 열거형.
public enum Fade
{
    Out = 0,
    In = 1
}

public class FadeScreen : MonoBehaviour
{
    public Image fadeImage;

    public event Action OnFinishedFadeOut;
    public event Action OnFinishedFadeIn;

    public void Fade(Fade fade)
    {
        if(fadeImage != null)
        {
            Color color = fadeImage.color;           

            if(fade == global::Fade.Out)
            {
                color.a = 0.0f;
                fadeImage.color = color;
                StartCoroutine(FadeOut());
            }
            else
            {
                color.a = 1.0f;
                fadeImage.color = color;
                StartCoroutine(FadeIn());
            }
        }
    }

    IEnumerator FadeOut()
    {
        while(true)
        {
            Color color = fadeImage.color;
            color.a += Time.unscaledDeltaTime;
            if(color.a > 1.0f)
            {
                color.a = 1.0f;
            }
            fadeImage.color = color;

            if(color.a == 1.0f)
            {
                break;
            }

            yield return null;
        }

        // 이벤트 함수가 등록이 되어 있을 경우에만 호출.
        if (OnFinishedFadeOut != null)
        {
            OnFinishedFadeOut.Invoke();
        }
    }

    IEnumerator FadeIn()
    {
        while (true)
        {
            Color color = fadeImage.color;
            color.a -= Time.unscaledDeltaTime;
            if (color.a < 0.0f)
            {
                color.a = 0.0f;
            }
            fadeImage.color = color;

            if (color.a == 0.0f)
            {
                break;
            }

            yield return null;
        }

        // 이벤트 함수가 등록이 되어 있을 경우에만 호출.
        if (OnFinishedFadeIn != null)
        {
            OnFinishedFadeIn.Invoke();
        }
    }
}
