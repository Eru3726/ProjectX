using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage2_Koya : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea;

    //ステージ２グレン戦の前後
    public static bool GurenFight = false;
    private bool CheckFirst = false;

    public static bool Stage2TextFinish = false;

    // Start is called before the first frame update
    void Start()
    {
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();
    }

    // Update is called once per frame
    void Update()
    {
        //グレン戦が終わったかどうかのフラグ(手動用)
        if (Input.GetKeyDown(KeyCode.I))
        {
            GurenFight = !GurenFight;
            CheckFirst = false;

        }

        if (!CheckFirst)
        {
            if (!GurenFight)
            {
                Debug.Log("初手");
                textNum = 14;
                GameUI.SetText(textNum);
            }
            else if (GurenFight)
            {
                Debug.Log("次");
                textNum = 15;
                GameUI.SetText(textNum);
            }
            CheckFirst = true;
        }
    }

    public void FinishBattle()
    {
        GurenFight = true;
        CheckFirst = false;
    }
}
