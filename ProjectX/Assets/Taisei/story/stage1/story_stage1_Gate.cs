using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class story_stage1_Gate : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea = false;
    [SerializeField] private story_stage1_Koya koyaFlg_1;
    public static bool gateFlg_2 = false;

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
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    textNum = 11;
                    GameUI.SetText(textNum);
                }

                if (!GameUI.checkText())
                {
                    //シーンチェンジ用
                    SceneManager.LoadScene("World");
                }

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    textNum = 10;
                    GameUI.SetText(textNum);
                    story_stage1_Koya.koyaFlg = true;
                    firstCheck = false;
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
        if (collision.gameObject.tag == "Player")
        {
            checkArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            checkArea = false;
        }
    }

    public void GateChange()
    {
        firstCheck = false;
    }

}
