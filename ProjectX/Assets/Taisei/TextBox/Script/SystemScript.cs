using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class SystemScript : MonoBehaviour
{
    //�g�[�NUI
    private Text messageText;

    //�\������e�L�X�g
    [SerializeField]
    [TextArea(1, 10)]
    private string allMessage = "�����RPG�ł悭�g���郁�b�Z�[�W�\���@�\����肽���Ǝv���܂��B\n"
            + "���b�Z�[�W���\�������X�s�[�h�̒��߂��\�ł���A���s�ɂ��Ή����܂��B\n"
            + "���P�̗]�n�����Ȃ肠��܂����A               �Œ���̋@�\�͔����Ă���Ǝv���܂��B\n"
            + "���Њ��p���Ă݂Ă��������B";

    //�g�p���镪��������
    [SerializeField] private string splitString = "<>";
    //���������e�L�X�g
    private string[] splitMessage;
    //�����������b�Z�[�W�̉��Ԗڂ�
    private int messageNum;
    //�e�L�X�g�X�s�[�h
    [SerializeField] private float textSpeed = 0.1f;
    //�o�ߎ���
    private float elapsedTime = 0f;
    //�����Ă��镶���ԍ�
    private int nowTextNum = 0;
    //�}�E�X�N���b�N�𑣂��A�C�R��
    private Image clickIcon;
    //�@�N���b�N�A�C�R���̓_�ŕb��
    [SerializeField]
    private float clickFlashTime = 0.2f;
    //�@1�񕪂̃��b�Z�[�W��\���������ǂ���
    private bool isOneMessage = false;
    //�@���b�Z�[�W�����ׂĕ\���������ǂ���
    private bool isEndMessage = false;

    //�L�����A�C�R��
    //�L�����A�C�R���̉摜��Assets�t�H���_����Resource�t�H���_�����A�����ɓ���邱��
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

    public AudioClip sound1;
    AudioSource audioSource;

    //�e�L�X�g�{�b�N�X��\�������ǂ���
    //false=�\�����ĂȂ��@true=�\����
    private bool TextOnOff = false;

    private float autoTimer = 0f;
    [SerializeField] float autoTimerLimit = 2f;

    [SerializeField] private bool AutoORAanual = false;

    // Start is called before the first frame update
    void Start()
    {
        clickIcon = transform.Find("TextPanel/Cursor").GetComponent<Image>();
        clickIcon.enabled = false;
        messageText = transform.GetChild(0).GetComponentInChildren<Text>();
        messageText.text = "";

        audioSource = GetComponent<AudioSource>();

        nameText = transform.GetChild(1).GetComponentInChildren<Text>();
        nameText.text = "";
        SetText(allMessage, allName, allIcon);


    }

    // Update is called once per frame
    void Update()
    {
        //message���I����Ă��邩�A���b�Z�[�W���Ȃ��ꍇ�͂���ȍ~�������Ȃ�
        if (isEndMessage || allMessage == null)
        {
            return;
        }

        //�P��ɕ\�����郁�b�Z�[�W��\�����Ă��Ȃ�
        if (!isOneMessage)
        {
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
            //�e�L�X�g�\�����Ԃ��o�߂����烁�b�Z�[�W��ǉ�
            if (elapsedTime >= textSpeed)
            {
                //���b�Z�[�W�\��
                messageText.text += splitMessage[messageNum][nowTextNum];
                audioSource.PlayOneShot(sound1);
                nowTextNum++;
                elapsedTime = 0f;

                //message��S���\���A�܂��͍s�����ő吔�\�����ꂽ
                if (nowTextNum >= splitMessage[messageNum].Length)
                {
                    isOneMessage = true;
                }
            }
            elapsedTime += Time.deltaTime;


            ////message�\�����ɃG���^�[����������ꊇ�\��
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    messageText.text += splitMessage[messageNum].Substring(nowTextNum);
            //    isOneMessage = true;
            //}

        }
        //�P��ɕ\�����郁�b�Z�[�W��\������
        else
        {
            if (AutoORAanual == false)
            {
                elapsedTime += Time.deltaTime;

                //�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
                if (elapsedTime >= clickFlashTime)
                {
                    clickIcon.enabled = !clickIcon.enabled;
                    elapsedTime = 0f;
                }

                //�G���^�[�L�[or���N���b�N���������玟�̕����\������
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                    nowNameNum = 0;
                    nameNum++;
                    nameText.text = "";
                    isOneMessage = false;
                    checkName = false;

                    iconNum++;

                    //message�����ׂĕ\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                autoTimer += Time.deltaTime;
                clickIcon.enabled = false;

                if (autoTimer >= autoTimerLimit)
                {
                    nowTextNum = 0;
                    messageNum++;
                    messageText.text = "";
                    clickIcon.enabled = false;
                    elapsedTime = 0f;
                    isOneMessage = false;

                    nowNameNum = 0;
                    nameNum++;
                    nameText.text = "";
                    isOneMessage = false;
                    checkName = false;

                    iconNum++;

                    autoTimer = 0f;

                    //message�����ׂĕ\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
                    if (messageNum >= splitMessage.Length)
                    {
                        isEndMessage = true;
                        TextOnOff = false;
                        transform.GetChild(0).gameObject.SetActive(false);
                        transform.GetChild(1).gameObject.SetActive(false);
                    }
                }
            }

            //elapsedTime += Time.deltaTime;

            ////�N���b�N�A�C�R����_�ł��鎞�Ԃ𒴂������A���]������
            //if (elapsedTime >= clickFlashTime)
            //{
            //    clickIcon.enabled = !clickIcon.enabled;
            //    elapsedTime = 0f;
            //}

            ////�G���^�[�L�[or���N���b�N���������玟�̕����\������
            //if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            //{
            //    nowTextNum = 0;
            //    messageNum++;
            //    messageText.text = "";
            //    clickIcon.enabled = false;
            //    elapsedTime = 0f;
            //    isOneMessage = false;

            //    nowNameNum = 0;
            //    nameNum++;
            //    nameText.text = "";
            //    isOneMessage = false;
            //    checkName = false;

            //    iconNum++;

            //    //message�����ׂĕ\������Ă�����Q�[���I�u�W�F�N�g���̂̍폜
            //    if (messageNum >= splitMessage.Length)
            //    {
            //        isEndMessage = true;
            //        TextOnOff = false;
            //        transform.GetChild(0).gameObject.SetActive(false);
            //        transform.GetChild(1).gameObject.SetActive(false);
            //    }
            //}
        }
    }

    void SetText(string message, string name, string icon)
    {
        this.allMessage = message;
        this.allName = name;
        this.allIcon = icon;
        //����������ň��ɕ\�����郁�b�Z�[�W�𕪊�����
        splitMessage = Regex.Split(allMessage, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitName = Regex.Split(allName, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        splitIcon = Regex.Split(allIcon, @"\s*" + splitString + @"\s*", RegexOptions.IgnorePatternWhitespace);
        nowTextNum = 0;
        messageNum = 0;
        messageText.text = "";
        iconNum = 0;
        nowNameNum = 0;
        nameNum = 0;
        nameText.text = "";
        isOneMessage = false;
        isEndMessage = false;
        TextOnOff = true;
    }

    //���̃X�N���v�g����V�������b�Z�[�W��ݒ肵�AUI���A�N�e�B�u�ɂ���
    public void SetTextPanel(string message, string name, string icon)
    {
        SetText(message, name, icon);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);

    }

    public bool CheckTextOnOff()
    {
        return TextOnOff;
    }
}
