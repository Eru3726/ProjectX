//�Ăяo�����b�Z�[�W��ݒ肷��

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkMessage : MonoBehaviour
{
    //TalkUI�ɐݒ肳��Ă���Message�X�N���v�g�EName�X�N���v�g��ݒ�
    [SerializeField]
    private TextSystem talkScript;
    

    //TalkUI��ݒ�
    [SerializeField]
    public GameObject obj;

    //�\�������郁�b�Z�[�W
    private string message = "����������\n" +
        "�Ȃɂʂ˂�<>" +
        "����������<>" +
        "��������������������������������<>" +
        "��������������������������������<>" +
        "��������������������������<>" +
        "��������������������������<>" +
        "��������������������������������";

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //obj��L����
            obj.SetActive(true);
            talkScript.SetMessagePanel(message);
        }
    }
}
