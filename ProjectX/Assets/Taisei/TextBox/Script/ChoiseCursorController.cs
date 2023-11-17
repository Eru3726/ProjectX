using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseCursorController : MonoBehaviour
{
    bool choise = true;
    bool choisefin = false;

    [SerializeField]
    private Image cursor;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70.5f, 24, 0);
            //�ړ���

            choise = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            cursor.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70.5f, -24, 0);
            //�ړ���

            choise = false;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameObject.SetActive(false);
            //���艹

            choisefin = true;
        }
    }

    public bool ChoiseFin()
    {
        return choisefin;
    }

    public bool Choise()
    {
        return choise;
    }
}
