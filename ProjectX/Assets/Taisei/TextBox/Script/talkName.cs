//���O�E�A�C�R���\���p

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkName : MonoBehaviour
{
    //TalkUI�ɐݒ肳��Ă���Name�X�N���v�g��ݒ�
    [SerializeField]
    private NameSystem nameScript;


    //�\�������閼�O
    private string charaName = "JK<>" +
        "???<>" +
        "��������<>" +
        "��������<>" +
        "�͂͂͂�<>" +
        "��<>" +
        "����������";
    //�\��������A�C�R����
    private string charaIcon = "��l������<>" +
        "��1<>" +
        "��l������<>" + "��l������<>" + "��l������<>" + "��l������<>" + "��l������" ;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            nameScript.SetNamePanel(charaName, charaIcon);

        }
    }
}
