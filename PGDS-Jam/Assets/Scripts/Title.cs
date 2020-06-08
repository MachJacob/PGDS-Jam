using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Image[] splash;
    public float fadeTime, stayTime;
    private float fade, stay;
    private int currSplash, state;

    private void Start()
    {
        currSplash = 0;
        state = 0;
        fade = 0;
        stay = 0;
    }

    void Update()
    {
        switch(state)
        {
            case 0:
                splash[currSplash].enabled = true;
                state = 1;
                break;
            case 1:
                fade += Time.deltaTime;
                splash[currSplash].color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fade) / fadeTime);
                if (currSplash >= 1)
                {
                    splash[currSplash - 1].color = new Color(1, 1, 1, Mathf.Lerp(0, 1, 1 - fade) / fadeTime);
                }
                if (fade >= fadeTime) state = 2;
                break;
            case 2:
                stay += Time.deltaTime;
                if (stay >= stayTime)
                {
                    state = 0;
                    currSplash++;
                    fade = 0;
                    stay = 0;
                }
                break;
            default:
                break;
        }
        if (currSplash == splash.Length)
        {
            state = -1;
            fade += Time.deltaTime;
            splash[currSplash - 1].color = new Color(1, 1, 1, Mathf.Lerp(0, 1, 1 - fade) / fadeTime);
            if (fade >= fadeTime)
            {
                foreach (Image im in splash)
                {
                    im.enabled = false;
                }
            }
        }
    }
    public void OpenGame()
    {
        SceneManager.LoadScene(1);
    }
}
