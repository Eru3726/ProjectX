using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLine_Title : MonoBehaviour
{
    [SerializeField] RectTransform[] STR_END;
    [SerializeField] GameObject[] fade;
    float x, y;
    int num = 0;

    Mng_Game Manager;
    void Start()
    {
        Manager = GameObject.Find("GameManager").GetComponent<Mng_Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) { return; }
        INPUTKEY();
        RtfChenge(num);
    }
    void INPUTKEY() // キー入力
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (num==1)
            {
                num--;
            }
            else
            {
                num = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
            if (num == 0)
            {
                num++;
            }
            else
            {
                num = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
            switch (num)
            {
                // スタート処理
                case 0:
                    GameStart();
                    break;
                // 終了処理
                case 1:
                    GameEnd();
                    break;
            }

        }
    }
    void RtfChenge(int i) // アウトラインの座標を変更
    {
        x = STR_END[i].anchoredPosition.x;
        y = STR_END[i].anchoredPosition.y;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }
    void GameStart()
    {
        fade[num].SetActive(true);
        this.gameObject.GetComponent<OutLine_Title>().enabled = false;
    }
    void GameEnd()
    {
        fade[num].SetActive(true);
        this.gameObject.GetComponent<OutLine_Title>().enabled = false;
    }
}
