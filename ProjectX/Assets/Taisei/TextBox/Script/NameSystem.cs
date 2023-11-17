using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NameSystem : MonoBehaviour
{

    //��������
    [SerializeField]
    private string splitString = "<>";
    //�@1�񕪂̃��b�Z�[�W��\���������ǂ���
    private bool isOneMessage = false;
    //�@���b�Z�[�W�����ׂĕ\���������ǂ���
    private bool isEndMessage = false;

    //�L�����A�C�R��
    private Image charaIcon;
    //�\������A�C�R���̖��O
    [SerializeField]
    [TextArea(1, 5)]
    private string allIcon = "��l������";
    //���������A�C�R����
    private string[] splitIcon;
    //���O�z��̉��Ԗڂ�
    private int iconNum;


    //���OUI
    private Text nameText;
    //�\�����閼�O
    [SerializeField]
    [TextArea(1, 5)]
    private string allName = "������<>" +
                           "������";
    //�����������O
    private string[] splitName;
    //���O�z��̉��Ԗڂ�
    private int nameNum;
    //�����Ă��閼�O�ԍ�
    private int nowNameNum = 0;
    //���O�̕\���������ǂ���
    private bool checkName = false;


    void Start()
    {
        nameText = transform.GetChild(1).GetComponentInChildren<Text>();
        nameText.text = "";
        SetName(allName, allIcon);
    }

    void Update()
    {
        //message���I����Ă��邩�A���b�Z�[�W���Ȃ��ꍇ�͂���ȍ~�������Ȃ�
        if (isEndMessage || allName == null)
        {
            return;
        }

        //�P��ɕ\�����閼�O��\�����Ă��Ȃ�
        if (!isOneMessage)
        {
            //���O�\��
            if (checkName == false)
            {
                //���O�\��
                nameText.text += splitName[nameNum].Substring(nowNameNum);
                checkName = true;

                //�A�C�R���\��
                Sprite sprite = Resources.Load<Sprite>(splitIcon[iconNum]) as Sprite;
                GameObject goImage = GameObject.Find("Icon");
                Image im = goImage.GetComponent<Image>();
                im.sprite = sprite;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                OneMessage();
            }

        }
        //�P��ɕ\�����郁�b�Z�[�W��\������
        else
        {
            //�G���^�[�L�[���������玟�̕����\������
            if (Input.GetKeyDown(KeyCode.Return))
            {
                nowNameNum = 0;
                nameNum++;
                nameText.text = "";
                isOneMessage = false;
                checkName = false;

                iconNum++;

            }
        }
    }


    public void SetName(string name, string icon)
    {
        this.allName = name;
        this.allIcon = icon;
        splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIcon = Regex.Split(allIcon, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        iconNum = 0;
        nowNameNum = 0;
        nameNum = 0;
        nameText.text = "";
        isOneMessage = false;
        isEndMessage = false;
    }

    public void SetNamePanel(string name, string icon)
    {
        SetName(name, icon);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OneMessage()
    {
        isOneMessage = true;
    }

}
