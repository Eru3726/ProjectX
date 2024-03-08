using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage2_Koya : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    [SerializeField] private GameObject kari;
    [SerializeField] private AllTexts all;
    [SerializeField] private TextScript ts;

    private bool checkArea;

    //ステージ２グレン戦の前後
    public static bool GurenFight = false;
    private bool CheckFirst = false;

    private bool checkBattleFin = false;
    private bool goAnim = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();
    }

    // Update is called once per frame
    void Update()
    {
        //グレン戦が終わったかどうかのフラグ(手動用)
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    GurenFight = !GurenFight;
        //    CheckFirst = false;

        //}

        if (checkBattleFin)
        {
            if (!ts.CheckTextOnOff())
            {
                goAnim = true;
            }
        }

        if (!CheckFirst)
        {
            if (!GurenFight)
            {
                textNum = 14;
                //GameUI.SetText(textNum);
                kari.SetActive(true);
                all.SetAllTexts(textNum);
            }
            else if (GurenFight)
            {
                textNum = 15;
                //GameUI.SetText(textNum);
                kari.SetActive(true);
                all.SetAllTexts(textNum);

                checkBattleFin = true;
            }
            CheckFirst = true;
        }
    }

    public void FinishBattle()
    {
        GurenFight = true;
        CheckFirst = false;
    }

    public bool FinishText()
    {
        return goAnim;
    }
}
