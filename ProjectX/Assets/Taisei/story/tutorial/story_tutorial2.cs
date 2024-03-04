using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class story_tutorial2 : MonoBehaviour
{
    private OpenOption GameUI;
    private int textNum;

    // Start is called before the first frame update
    void Start()
    {
        GameUI = GameObject.Find("GameUI").GetComponent<OpenOption>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            textNum = 1;
            GameUI.SetText(textNum);
        }
    }
}
