using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderEffect : MonoBehaviour
{
    [SerializeField] private Image Flash_Image;
    [SerializeField] private Image Thunder_Image;
    private float Flash_alpha;
    private float Thunder_alpha;

    private bool Flash_Check = false;
    private bool Thunder_Check = false;
    [SerializeField] private float Flash_NumIn = 0.1f;
    [SerializeField] private float Flash_NumOut = 0.25f;
    [SerializeField] private float Thunder_NumIn = 0.06f;
    [SerializeField] private float Thunder_NumOut = 0.2f;

    private bool kariF = false;
    private bool kariT = false;

    void Start()
    {
        Flash_alpha = Flash_Image.color.a;
        Thunder_alpha = Thunder_Image.color.a;

        Flash_Image.color = new Color(255, 255, 255, 0);
        Thunder_Image.color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        //手動起動用
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    kariF = true;
        //    kariT = true;
        //}


        if (kariF)
        {
            //フラッシュ
            if (Flash_Check)
            {
                Flash_FadeIn();
            }
            else
            {
                Flash_FadeOut();
            }
        }
        if (kariT)
        {
            //雷
            if (Thunder_Check)
            {
                Thunder_FadeIn();
            }
            else
            {
                Thunder_FadeOut();
            }
        }

    }

    //フラッシュ
    private void Flash_FadeIn()
    {
        Flash_alpha -= Flash_NumIn;
        Flash_Image.color = new Color(255, 255, 255, Flash_alpha);
        if (Flash_alpha <= 0)
        {
            Flash_Check = false;
            kariF = false;
        }
    }

    private void Flash_FadeOut()
    {
        Flash_alpha += Flash_NumOut;
        Flash_Image.color = new Color(255, 255, 255, Flash_alpha);
        if (Flash_alpha >= 1)
        {
            Flash_Check = true;
        }
    }

    //雷
    private void Thunder_FadeIn()
    {
        Thunder_alpha -= Thunder_NumIn;
        Thunder_Image.color = new Color(255, 255, 255, Thunder_alpha);
        if (Thunder_alpha <= 0)
        {
            Thunder_Check = false;
            kariT = false;
        }
    }

    private void Thunder_FadeOut()
    {
        Thunder_alpha += Thunder_NumOut;
        Thunder_Image.color = new Color(255, 255, 255, Thunder_alpha);
        if (Thunder_alpha >= 1)
        {
            //音流すとこ

            Thunder_Check = true;
        }
    }

    //起動する用
    public void OnEffect()
    {
        kariF = true;
        kariT = true;
    }
}
