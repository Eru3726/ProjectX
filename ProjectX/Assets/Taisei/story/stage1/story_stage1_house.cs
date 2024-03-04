using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage1_house : MonoBehaviour
{
    private int textNum;
    private OpenOption GameUI;

    private bool checkArea = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (checkArea)
        {
            //仮設定の調べるコマンドキー
            if (Input.GetKeyDown(KeyCode.O))
            {
                textNum = 5;
                GameUI.SetText(textNum);
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
