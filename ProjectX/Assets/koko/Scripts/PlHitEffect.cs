using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlHitEffect : MonoBehaviour
{
    [SerializeField, Header("hitcolliderアタッチ")]
    PlHitCol hc;

    [SerializeField, Header("amariアタッチ")]
    GameObject amari;

    SpriteRenderer sr;

    bool isBlink = false;
    float blinkTime = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        amari.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (isBlink)
        {
            amari.SetActive(false);
        }
        else
        {
            amari.SetActive(true);
        }

        if (hc.time > 0)
        {
            blinkTime += Time.deltaTime;

            if (blinkTime >= 0.1f)
            {
                if (isBlink)
                {
                    isBlink = false;
                }
                else
                {
                    isBlink = true;
                }
                blinkTime = 0;
            }
        }
        else
        {
            isBlink = false;
        }
    }

}
