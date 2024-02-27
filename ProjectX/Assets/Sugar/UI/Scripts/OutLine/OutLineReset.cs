using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineReset : MonoBehaviour
{
    [SerializeField] GameObject MenuOut;
    void OnEnable()
    {
        MenuOut.GetComponent<OutLine_Menu>().enabled = true;
        MenuOut.GetComponent<OutLine_Menu>().num = 0;
        MenuOut.GetComponent<OutLine_Menu>().targetRot = 0;
    }
}
