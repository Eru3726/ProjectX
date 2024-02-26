using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choise : MonoBehaviour
{
    [SerializeField] RectTransform[] ChoiseCursor;
    private int ListNum;
    float x, y;
    [SerializeField] private RectTransform thisObj;

    [SerializeField] GameObject PareObj;

    //選択肢フラグ
    private int choiseFlg = 0;

    [SerializeField] private TextScript textScript;


    // Start is called before the first frame update
    void Start()
    {
        ListNum = 0;
        CursorMove();
        //PareObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (textScript.logCheck)
        {
            return;
        }
        //カーソルの上下移動
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ListNum--;
            if (ListNum < 0)
            {
                ListNum = ChoiseCursor.Length - 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
        {
            ListNum++;
            if (ListNum > ChoiseCursor.Length - 1)
            {
                ListNum = 0;
            }
        }
        CursorMove();

        //決定押したとき
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("決定");
            choiseFlg = ListNum;
            textScript.ChangeCheckChoise();

            ListNum = 0;
            PareObj.SetActive(false);
        }
    }

    //カーソル座標変更
    private void CursorMove()
    {
        x = ChoiseCursor[ListNum].anchoredPosition.x;
        y = ChoiseCursor[ListNum].anchoredPosition.y;
        thisObj.anchoredPosition = new Vector2(x, y);
    }

    public int ChoiseFlg()
    {
        return choiseFlg;
    }
}
