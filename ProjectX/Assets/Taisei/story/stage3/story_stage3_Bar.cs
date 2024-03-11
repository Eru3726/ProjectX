using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage3_Bar : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea = false;

    void Start()
    {
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();
    }

    void Update()
    {
        
    }
}
