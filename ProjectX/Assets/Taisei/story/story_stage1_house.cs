using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage1_house : MonoBehaviour
{
    //仮
    [SerializeField] private GameObject GameUI_kari;
    [SerializeField] private AllTexts allTexts;

    private int textNum;
    private OpenOption GameUI;

    private bool checkArea = false;

    // Start is called before the first frame update
    void Start()
    {
        //仮処理
        checkArea = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            textNum = 5;
            GameUI_kari.SetActive(true);
            allTexts.SetAllTexts(textNum);
            //GameUI.SetText(textNum)
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
