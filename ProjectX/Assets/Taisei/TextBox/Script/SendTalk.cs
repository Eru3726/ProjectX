using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTalk : MonoBehaviour
{
    /*�ȉ��̓��e������΃��b�Z�[�W�𑗂邱�Ƃ��ł���*/

    //TalkUI�ɐݒ肳��Ă���Message�X�N���v�g�EName�X�N���v�g��ݒ�
    [SerializeField]
    private SystemScript TextSystemScript;

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


    //�\�������閼�O
    private string charaName = "JK<>" +
        "???<>" +
        "��������<>" +
        "��������<>" +
        "�͂͂͂�<>" +
        "��<>" +
        "����������";

    //�\��������A�C�R�����@���A�C�R�����͉摜���Ɠ����ɂ��邱��
    private string charaIcon = "��l������<>" +
        "��1<>" +
        "��l������<>" + "��l������<>" + "��l������<>" + "��l������<>" + "��l������";

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            obj.SetActive(true);
            //�\���������e�L�X�g�A�L�������A�L�����A�C�R���𑗂�
            TextSystemScript.SetTextPanel(message, charaName, charaIcon);
        }
    }

    /*�ȏ�̓��e������΃��b�Z�[�W�𑗂邱�Ƃ��ł���*/
}
