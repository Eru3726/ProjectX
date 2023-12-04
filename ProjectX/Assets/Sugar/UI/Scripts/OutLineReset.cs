using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineReset : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField]float x, y;
    [SerializeField] float rx, ry, rz;
    private void Start()
    {
    }
    void OnEnable()
    {
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
        obj.GetComponent<RectTransform>().rotation= Quaternion.Euler(rx, ry, rz);
    }
}
