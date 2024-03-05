using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage2_Koya : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea;

    //ステージ２グレン戦の前後
    public bool GurenFight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GurenFight = !GurenFight;
        }
        if (checkArea)
        {
            if (!GurenFight)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 14;
                    GameUI.SetText(textNum);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 15;
                    GameUI.SetText(textNum);

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkArea = false;
        }
    }

}
