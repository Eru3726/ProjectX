using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTexts : MonoBehaviour
{
    [SerializeField]
    private SystemScript TextSystemScript;

    //�\�������郁�b�Z�[�W
    private string message;


    //�\�������閼�O
    private string charaName;

    //�\��������A�C�R�����@���A�C�R�����͉摜���Ɠ����ɂ��邱��
    private string charaIcon;


    //�g����
    //�E�����Ƀe�L�X�g�E���O�E�A�C�R�����Ȃǂ���������ł���
    //�E�䎌�E���O�E�A�C�R�����������ňꊇ�Ǘ�����
    //�E�u<>�v������Ƃ��܂ł��ЂƂ̑䎌�ƂȂ�
    //�E��ԍŌ�̂Ƃ���Ɂu<>�v������Ɠ��삪��肭�����Ȃ��Ȃ邽�߁A
    //�@���Ȃ��悤�ɒ��ӂ��Ă�������
    //�Easset�t�H���_�[��Resources�t�H���_�[�����A�����ɃA�C�R���p�̉摜������
    //�E�����ɏ����A�C�R�����̓A�C�R���p�摜�̖��O�Ɠ����ɂ���
    //
    //�\��������Ƃ�
    //�X�N���v�g�Ɉȉ��̕���ǉ�
    //[SerializeField] private AllTexts alltextsscript;
    //[SerializeField] private GameObject talkUI;
    //int textNo;
    //���͂ǂ���Ƃ��Q�[���I�u�W�F�N�g�́uTalkUI�v��ݒ肷��
    //
    //�ȉ��̕���update�Ȃǂɒǉ����邱�Ƃő䎌�Ȃǂ�\������
    //textNo = ���Z;���Ăяo�������Z���t�̔ԍ���ݒ�
    //talkUI.SetActive(true);
    //alltextsscript.SetAllTexts(textNo);
    //
    //���g�p�ၨ�utestSet�v�X�N���v�g


    public void SetAllTexts(int textNo)
    {
        Debug.Log("��");
        switch (textNo)
        {
            case 0:
                message = "����ɂ���<>" +
                    "����̓e�X�g�ł�<>" +
                    "���s�����\n�����Ȃ�܂��B";
                charaName = "��<>" +
                    "(�L�E�ցE�M)<>" +
                    "�₟";
                charaIcon = "��l������<>" +
        "��1<>" +
        "��l������<>";
                Debug.Log("����");
                break;

            case 1:
                message = "����΂��<>" +
                    "��2�̃e�X�g�ł�";
                charaName = "�H�H�H<>" +
                    "���m";
                charaIcon = "��2<>" +
                    "��3";
                break;
        }

        TextSystemScript.SetTextPanel(message, charaName, charaIcon);

    }
}
