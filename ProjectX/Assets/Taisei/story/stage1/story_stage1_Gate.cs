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

    [SerializeField] private GameObject Lock;
    [SerializeField] private GameObject Open;
    private bool firstCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();
    }

    // Update is called once per frame
    void Update()
    {

        if (checkArea)
        {
            if (gateFlg_2)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 11;
                    GameUI.SetText(textNum);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    textNum = 10;
                    GameUI.SetText(textNum);
                    koyaFlg_1.koyaFlg = true;
                    firstCheck = false;

                }

                if (!GameUI.checkText())
                {
                    //シーンチェンジ用
                }
            }
        }

        if (!firstCheck)
        {
            if (!gateFlg_2)
            {
                Lock.SetActive(true);
                Open.SetActive(false);
            }
            else
            {
                Lock.SetActive(false);
                Open.SetActive(true);
            }
            firstCheck = true;
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
