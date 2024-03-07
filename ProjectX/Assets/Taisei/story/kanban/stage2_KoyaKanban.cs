using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage2_KoyaKanban : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    private bool checkArea = false;


    // Start is called before the first frame update
    void Start()
    {
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkArea)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                textNum = 13;
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
