using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class story_tutorial : MonoBehaviour
{
    [SerializeField] private GameObject FadePanel;
    Image fadeAlpha;
    private float alpha;

    private bool fadeIn = false;
    private bool fadeOut = false;

    private OpenOption GameUI;
    private int textNum;

    private bool startStory = false;
    private bool tutorialcheck = false;

    private bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeAlpha = FadePanel.GetComponent<Image>();
        alpha = fadeAlpha.color.a;
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();

        textNum = 2;
        GameUI.SetText(textNum);

    }

    // Update is called once per frame
    void Update()
    {
        if (finish)
        {
            return;
        }
        if (!startStory)
        {
            if (!GameUI.checkText())
            {
                startStory = true;
            }
        }
        else
        {
            if (!fadeIn)
            {
                FadeIn();
            }
            if (fadeIn)
            {
                textNum = 3;
                GameUI.SetText(textNum);
                if (!GameUI.checkText())
                {
                    tutorialcheck = true;
                }
            }
        }

        if (tutorialcheck)
        {
            if (!GameUI.checkText())
            {
                textNum = 0;
                GameUI.SetText(textNum);
                finish = true;
            }
        }

    }

    //明転
    private void FadeIn()
    {
        alpha -= 0.01f;
        fadeAlpha.color = new Color(0, 0, 0, alpha);

        if (alpha <= 0)
        {
            fadeIn = true;
        }
    }

    //暗転
    private void FadeOut()
    {
        alpha += 0.01f;
        fadeAlpha.color = new Color(0, 0, 0, alpha);

        if (alpha >= 1)
        {
            fadeOut = true;
            fadeIn = false;
        }
    }
}
