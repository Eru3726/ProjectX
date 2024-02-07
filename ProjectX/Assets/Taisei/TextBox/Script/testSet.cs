using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSet : MonoBehaviour
{

    [SerializeField] private AllTexts alltextsscript;
    [SerializeField] private GameObject talkUI;

    int textNo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            textNo = 999;
            talkUI.SetActive(true);
            alltextsscript.SetAllTexts(textNo);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            textNo = 999;
            talkUI.SetActive(true);
            alltextsscript.SetAllTexts(textNo);
        }
    }
}
