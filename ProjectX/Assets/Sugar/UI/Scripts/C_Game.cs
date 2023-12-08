using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class C_Game : MonoBehaviour
{
    // これが表示されている間動かない
    [SerializeField] GameObject panel;

    [SerializeField] RectTransform[] List;
    [SerializeField] RectTransform[] textspd;

    RectTransform obj;

    [SerializeField] GameObject ConfigP;
    float x, y;
    // textspd用
    int num = 1;
    int Copynum=1;
    // List用
    int Listnum = 0;
    // switch
    int method = 0;

    // Slow:0.08 Normal:0.04 Fast:0.02
    float Set_textSpd =0.04f;
    float Send_textSpd =0.04f;

    private void Start()
    {
        obj = this.gameObject.GetComponent<RectTransform>();
        Debug.Log("A");
    }
    private void Update()
    {
        Debug.Log(Send_textSpd);
        if (panel.activeSelf == true)
        {
            return;
        }
        move();
    }

    void move()
    {
        switch (method)
        {
            case 0:
                InputKey(List.Length, method);
                List_pos();
                break;
            case 1:
                InputKey(textspd.Length, method);
                TextSpd_posChange();
                break;
        }
    }
    private void OnEnable()
    {
        Listnum = 0;
        method = 0;
        for (int i = 0; i < textspd.Length; i++)
        {
            textspd[i].GetComponent<Text>().enabled = false;
        }
        x = List[Listnum].anchoredPosition.x;
        y = List[Listnum].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = List[Listnum].rotation;
        num = Copynum;
    }
    void InputKey(int box,int method0)
    {
        switch(method0)
        {
            case 0:
                // 上入力
                if (Input.GetKeyDown(KeyCode.W) && Listnum > 0)
                { Listnum--; }
                // 下入力
                else if (Input.GetKeyDown(KeyCode.S) && Listnum < box - 1)
                { Listnum++; }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (Listnum == 0)
                    {
                        method = 1;
                        for (int i = 0; i < textspd.Length; i++)
                        {
                            textspd[i].GetComponent<Text>().enabled = true;
                        }
                    }
                    else
                    {
                        Copynum = num;
                        Send_textSpd = Set_textSpd;
                        panel.SetActive(true);
                        ConfigP.GetComponent<Config>().enabled = true;
                        this.gameObject.GetComponent<C_Game>().enabled = false;
                        Listnum = 0;
                    }
                }
                break;
            case 1:
                // 上入力
                if (Input.GetKeyDown(KeyCode.W) && num > 0)
                { num--; }
                // 下入力
                else if (Input.GetKeyDown(KeyCode.S) && num < box - 1)
                { num++; }

                if (Input.GetKeyDown(KeyCode.Return))
                {   
                    for(int i=0;i<textspd.Length;i++)
                    {
                        textspd[i].GetComponent<Text>().enabled = false;
                    }
                    method = 0;
                }
                break;
        }
    }
    void List_pos()
    {
        x = List[Listnum].anchoredPosition.x;
        y = List[Listnum].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = List[Listnum].rotation;
    }
    void TextSpd_posChange()
    {
        x = textspd[num].anchoredPosition.x;
        y = textspd[num].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = textspd[num].rotation;
        switch(num)
        {
            case 0:
                Set_textSpd = 0.08f;
                break;
            case 1:
                Set_textSpd = 0.04f;
                break;
            case 2:
                Set_textSpd = 0.02f;
                break;
        }
    }
   
}

