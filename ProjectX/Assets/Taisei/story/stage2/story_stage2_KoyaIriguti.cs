using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class story_stage2_KoyaIriguti : MonoBehaviour
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
        if (checkArea)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                textNum = 54;
                GameUI.SetText(textNum);
            }

            if (!GameUI.checkText())
            {
                if (GameUI.checkRoute() == 0)
                {
                    //シーン移動
                    SceneManager.LoadScene("Room1");
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
