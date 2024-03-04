using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEffect : MonoBehaviour
{
    SpriteRenderer sr;

    [SerializeField]
    float waitTime = 0;

    [SerializeField]
    float transTime = 1;

    float trans = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        trans = 255;
    }

    private void Update()
    {
        sr.color = new Color32(255, 255, 255, (byte)trans);

        waitTime -= Time.deltaTime;

        if (waitTime <= 0)
        {
            trans -= Time.deltaTime * (255 / transTime);
        }

        if (trans <= 0)
        {
            Destroy(gameObject);
        }
    }
}
