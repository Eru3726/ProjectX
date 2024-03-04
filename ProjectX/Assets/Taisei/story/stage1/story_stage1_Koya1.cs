using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_stage1_Koya1 : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

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
            if (Input.GetKeyDown(KeyCode.O))
            {
                textNum = 7;
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
