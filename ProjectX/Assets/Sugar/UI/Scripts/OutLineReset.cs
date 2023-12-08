using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineReset : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField]float x, y;
    [SerializeField] float rx, ry, rz;
    [SerializeField] GameObject MenuOut;
    void OnEnable()
    {
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
        obj.GetComponent<RectTransform>().rotation= Quaternion.Euler(rx, ry, rz);
        MenuOut.GetComponent<OutLine_Menu>().enabled = true;
    }
}
