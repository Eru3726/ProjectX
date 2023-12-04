using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Config : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] Text ConfigText;
    private string[] Text = { "DISPLAY", "AUDIO", "KEYCONFIG" };

    [Header("0:ディスプレイ項目\n" +
       " 1:オーディオ項目\n" +
       " 2:操作設定")]
    [SerializeField] GameObject[] List;

    [SerializeField] GameObject OutLine;
    [SerializeField] GameObject Panel;

    int InputNum=0;
    private void OnEnable()
    {
        // 最初の項目に設定
        InputNum = 0;
        OutLine.SetActive(true);
        Panel.SetActive(true);
    }
    void Update()
    {
        // 入力
        InputKEY();
        ObjActive(InputNum);
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
        }
        List[num].SetActive(true);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OutLine.SetActive(false);
            Panel.SetActive(false);
        }
    }
}
