using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage1_Koya : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea = false;

    //フラグ
    public bool koyaFlg = false;

    [SerializeField] private story_stage1_Gate gateFlg_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkArea)
        {
            if (koyaFlg)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 8;
                    GameUI.SetText(textNum);
                    gateFlg_2.gateFlg_2 = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 7;
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
