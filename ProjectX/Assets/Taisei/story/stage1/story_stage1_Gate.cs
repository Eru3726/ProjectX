using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage1_Gate : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea = false;
    [SerializeField] private story_stage1_Koya koyaFlg_1;
    public bool gateFlg_2 = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkArea)
        {
            if (gateFlg_2)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    textNum = 11;
                    GameUI.SetText(textNum);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    textNum = 10;
                    GameUI.SetText(textNum);
                    koyaFlg_1.koyaFlg = true;
                }

                if (!GameUI.checkText())
                {
                    //シーンチェンジ用
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
