using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animalUnlockAnimation : MonoBehaviour
{
    [SerializeField] Image cover;

    float timer = 3;
    float startAlpha;
    float curAlpha;

    private void Start()
    {
        startAlpha = cover.color.a;
        curAlpha = startAlpha;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.unscaledDeltaTime;

        if (timer < 2.5f && timer > 0.5f)
        {
            curAlpha -= Time.unscaledDeltaTime * startAlpha / 2;
            cover.color = new Color(cover.color.r, cover.color.g, cover.color.b, curAlpha);
        }
        else if (timer < 0)
        {
            gameObject.SetActive(false);
            togglePause(false);
        }
    }

    public void doTheThing()
    {
        gameObject.SetActive(true);
        togglePause(true);
    }

    void togglePause(bool yes)
    {
        if (!yes)
        {
            //unpause
            Time.timeScale = 1;
        }
        else
        {
            //pause
            Time.timeScale = 0;
        }
    }
}
