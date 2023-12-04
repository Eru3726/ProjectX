using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOption : MonoBehaviour
{
    [SerializeField] GameObject MenuObj;
    // フェード中のボタン制御
    [SerializeField] GameObject[] fadeList;
    bool FADE = false;
    // Escキーの連打対策
    bool interval = true;
    // タイミングように
    bool MoveUI = false;
    // 移動速度
    [SerializeField] float spdX;
    [SerializeField] float spdY;
    void Start()
    {
        // マウスカーソル削除
        Cursor.visible = false;
        // メニュー画面を閉じておく
        MenuObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // 書き方汚いいつか直して
        if (fadeList[0].activeSelf == true) { return; }
        else if (fadeList[1].activeSelf == true) { return; }
        else if (fadeList[2].activeSelf == true) { return; }
        // Escキーを押したらメニュー起動
        // もう一度押したらとじる
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuObj.activeSelf==false&&interval) 
            { 
                MenuObj.SetActive(true);
                MoveUI = true;
                
            }
            else if(MenuObj.activeSelf==true&&interval)
            {
                MenuObj.SetActive(false);
            }
        }
        if(MoveUI)
        {
            ColStart();
        }
    }
    void ColStart()
    {
        interval = false;
        // (0,0)座標まで
        if (MenuObj.GetComponent<RectTransform>().anchoredPosition 
            != new Vector2(0, 0))
        {
            StartCoroutine("moveS");
        }
        else 
        {
            interval = true;
            MoveUI = false;
            StopCoroutine("moveS");
        }
    }
    IEnumerator moveS()
    {
        //ここに処理を書く
        MenuObj.GetComponent<RectTransform>().anchoredPosition-=new Vector2(spdX,spdY);

        yield return null;
    }
}
