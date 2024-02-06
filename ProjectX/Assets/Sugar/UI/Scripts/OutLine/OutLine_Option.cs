using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OutLine_Option : MonoBehaviour
{
    RectTransform rtf;
    // �ݒ荀��
    [SerializeField] GameObject[] item;
    [SerializeField] Text[] text;
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    void Update()
    {
        // �����
        if (Input.GetKeyDown(KeyCode.W)&&
            rtf.anchoredPosition != new Vector2(150,-50)) 
        { rtf.anchoredPosition += new Vector2(0, 100); }
        // ������
        else if (Input.GetKeyDown(KeyCode.S)&& 
            rtf.anchoredPosition != new Vector2(150,-250)) 
        { rtf.anchoredPosition += new Vector2(0, -100); }

        // ����ݒ荀��
        if      (rtf.anchoredPosition == new Vector2(150, -50))
        { ObjActive(0); }
        // �f�B�X�v���C����
        else if (rtf.anchoredPosition == new Vector2(150, -150))
        { ObjActive(1); }
        // �I�[�f�B�I����
        else if (rtf.anchoredPosition == new Vector2(150, -250))
        { ObjActive(2); }
    }

    // �S�Ĕ�\���ɂ��Ă������̃I�u�W�F�N�g��\��
    void ObjActive(int num)
    {
       for(int i=0;i<item.Length;i++)
        {
            item[i].SetActive(false);
            text[i].fontStyle = FontStyle.Normal;
        }
        item[num].SetActive(true);
        text[num].fontStyle = FontStyle.Bold;
    }
}
