using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Config : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] Text ConfigText;
    private string[] Text = { "Game", "AUDIO", "KEYCONFIG" };

    [Header("0:ゲームプレイ項目\n" +
       " 1:オーディオ項目\n" +
       " 2:操作設定")]
    [SerializeField] GameObject[] List;
    [Header("今選んでる項目を分かりやすくするためのもの")]
    [SerializeField] Image[] Listpoint;
    [SerializeField] GameObject[] List_OutlineScr;

    [SerializeField] GameObject OutLine;
    [SerializeField] GameObject Panel;

    [SerializeField] Text[] rsText;
    [SerializeField] GameObject[] rsitem;

    int InputNum=0;
    private void OnEnable()
    {
        // 最初の項目に設定
        // InputNum = 0;
        OutLine.SetActive(true);
        Panel.SetActive(true);
        for(int i=0;i<rsText.Length;i++)
        {
            rsText[i].GetComponent<Text>().enabled = false;
        }
        for (int i = 0; i < rsitem.Length; i++)
        {
            rsitem[i].SetActive(false);
        }
    }
    void Update()
    {
        // 入力
        InputKEY();
        ObjActive(InputNum);
        Debug.Log(InputNum);
    }

    void InputKEY()
    {
        // アウトラインが消えたら操作不可
        if (OutLine.activeSelf == false) { return; }
        if(Input.GetKeyDown(KeyCode.D))
        {
            // 最大値より上にならないように
            if (InputNum == Text.Length-1) { InputNum = 0; return; }
            InputNum++;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            // 最小値
            if (InputNum == 0) { InputNum = Text.Length-1; return; }
            InputNum--;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            // メニュー画面に戻る
            Menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
        if (ConfigText.text != Text[InputNum])
        {
            ConfigText.text = Text[InputNum];
        }
        //Debug.Log(Text.Length);
    }
    // 全て非表示にしてから特定のオブジェクトを表示
    void ObjActive(int num)
    {
        for (int i = 0; i < List.Length; i++)
        {
            List[i].SetActive(false);
            Listpoint[i].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);// 仮のサイズ 
        }
        List[num].SetActive(true);
        Listpoint[num].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);// 仮のサイズ 
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // スクリプトを起動
            switch (num)
            {
                case 0: // ゲーム
                    List_OutlineScr[num].GetComponent<C_Game>().enabled = true;
                    List_OutlineScr[num].GetComponent<Image>().enabled = true;
                    break;

                case 1: // オーディオ
                    List_OutlineScr[num].GetComponent<C_Audio>().enabled = true;
                    List_OutlineScr[num].GetComponent<Image>().enabled = true;
                    break;
                case 2: // 操作

                    break;

            }
            OutLine.SetActive(false);
            Panel.SetActive(false);
            this.gameObject.GetComponent<Config>().enabled = false;
        }
    }
}
