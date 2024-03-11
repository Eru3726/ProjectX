using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warota : MonoBehaviour
{
    GameObject pl;
    PlHitCol phc;

    private void Start()
    {
        pl = GameObject.Find("Player");
        phc = pl.transform.GetChild(0).gameObject.GetComponent<PlHitCol>();
    }

    private void Update()
    {

        float hp = (float)phc.nowHp / (float)phc.maxHp;

        //Debug.Log(phc.nowHp);
        //Debug.Log(phc.maxHp);

        Debug.Log(hp);

        Vector3 scale = transform.localScale;
        scale.x = hp;
        transform.localScale = scale;

        Vector3 aPos = GetComponent<RectTransform>().anchoredPosition;
        aPos.x = 230 * hp;
        GetComponent<RectTransform>().anchoredPosition = aPos;

        //Vector3 pos = transform.position;
        //pos.x = 0;
        //transform.position = pos;
    }
}
