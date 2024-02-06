using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOption : MonoBehaviour
{
    [SerializeField] RectTransform under; 
    [SerializeField] GameObject MenuObj;
    // フェード中のボタン制御
    [SerializeField] GameObject[] fadeList;
    // Escキーの連打対策
    bool interval = true;
    // タイミングように
    bool MoveUI = false;

    // 一定時間操作がなかった時にアンダーを表示
    float timer = 0;
    // UIの移動速度
    [SerializeField] float spdX;
    [SerializeField] float spdY;

    [SerializeField] float spdX_under;
    [SerializeField] float spdY_under;
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
        // 書き方汚いのでいつか修正します。
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

        // キー入力なしの時アンダーライン表示
        if(!Input.anyKey)
        {
            timer += Time.deltaTime;
            if (under.anchoredPosition != new Vector2(0, 0) && timer >= 5.0f)
            {
                spdX_under = 0;
                spdY_under = 10.0f;
                StartCoroutine("moveUnder");
            }
            else if (spdY_under>0)
            {
                StopCoroutine("moveUnder");
            }
        }
        else
        {
            timer = 0;
            if (under.anchoredPosition != new Vector2(0, -100))
            {
                spdX_under = 0;
                spdY_under = -10.0f;
                StartCoroutine("moveUnder");
            }
            else
            {
                StopCoroutine("moveUnder");
            }
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
    IEnumerator moveUnder()
    {
        //ここに処理を書く
        under.anchoredPosition += new Vector2(spdX_under, spdY_under);

        yield return null;
    }
}
