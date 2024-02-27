using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class C_Audio : MonoBehaviour
{
    // 音源
    [SerializeField] Mng_Game Manager;

    // 音量変更
    [SerializeField] Mng_Game para;

    // これが表示されている間動かない
    [SerializeField] GameObject panel;

    // 選択項目
    [SerializeField] RectTransform[] List;

    // マスター
    [SerializeField] RectTransform[] Master_Rect; // ボタン位置情報
    [SerializeField] Image[] Master_Image;        // 音量バーの表示設定
    [SerializeField] Text Master_Now;             // 現在の音量値
    [SerializeField] GameObject _Master;          // 項目の親オブジェクト     
    int Master_Num = 0;                           // 入力
    int Master_Now_Num = 5;                       // 初期値
    int Copy_Master_Now_Num = 5;                      // 確定するまでの保存用
    // BGM
    [SerializeField] RectTransform[] BGM_Rect;
    [SerializeField] Image[] BGM_Image;
    [SerializeField] Text BGM_Now;
    [SerializeField] GameObject _BGM;
    int BGM_Num = 0;
    int BGM_Now_Num = 5;
    int Copy_BGM_Now_Num = 5;
    // SE
    [SerializeField] RectTransform[] SE_Rect;
    [SerializeField] Image[] SE_Image;
    [SerializeField] Text SE_Now;
    [SerializeField] GameObject _SE;
    int SE_Num = 0;
    int SE_Now_Num = 5;
    int Copy_SE_Now_Num = 5;

    RectTransform obj;

    [SerializeField] GameObject ConfigP;
    float x, y;
    // List用
    int Listnum = 0;
    // switch
    int method = 0;

    float wid = 0;
    float hei = 0;
    private void Start()
    {
        obj = this.gameObject.GetComponent<RectTransform>();
        Debug.Log("B");

        // 初期化用
        wid = obj.rect.width;
        hei = obj.rect.height;
    }
    private void Update()
    {
        if (panel.activeSelf == true)
        {
            return;
        }
        move();
        ActBar();
    }

    private void OnEnable()
    {
        Listnum = 0;
        Master_Num = 0;
        BGM_Num = 0;
        SE_Num = 0;
        method = 0;

        Master_Now_Num = Copy_Master_Now_Num;
        BGM_Now_Num = Copy_BGM_Now_Num;
        SE_Now_Num = Copy_SE_Now_Num;

        _Master.SetActive(false);
        _BGM.SetActive(false);
        _SE.SetActive(false);

        this.gameObject.GetComponent<Image>().enabled = false;
       
        x = List[Listnum].anchoredPosition.x;
        y = List[Listnum].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        obj.rotation = List[Listnum].rotation;
    }
    void move()
    {
        switch (method)
        {
            // 選択
            case 0:
                InputKey(List.Length, method);
                _pos(List,Listnum);
                break;
            // マスター
            case 1:
                InputKey(Master_Rect.Length, method);
                _pos(Master_Rect,Master_Num);
                image(Master_Image, Master_Now, Master_Now_Num);
                break;
            // BGM
            case 2:
                InputKey(BGM_Rect.Length, method);
                _pos(BGM_Rect, BGM_Num);
                image(BGM_Image, BGM_Now, BGM_Now_Num);
                break;
            // SE
            case 3:
                InputKey(SE_Rect.Length, method);
                _pos(SE_Rect, SE_Num);
                image(SE_Image, SE_Now, SE_Now_Num);
                break;
        }
    }
    void ActBar()
    {
        if (Listnum == 0)
        {
            _Master.SetActive(true);
            _BGM.SetActive(false);
            _SE.SetActive(false);
        }
        // BGM
        else if (Listnum == 1)
        {
            _BGM.SetActive(true);
            _Master.SetActive(false);
            _SE.SetActive(false);
        }
        // SE
        else if (Listnum == 2)
        {
            _SE.SetActive(true);
            _BGM.SetActive(false);
            _Master.SetActive(false);
        }
        else if (Listnum == 3) // 完了ボタン
        {
            _SE.SetActive(false);
            _BGM.SetActive(false);
            _Master.SetActive(false);
        }
    }
    void InputKey(int box, int method0)
    {
        switch (method0)
        {
            case 0:
                // 上入力
                if (Input.GetKeyDown(KeyCode.W) && Listnum > 0)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    Listnum--;
                }
                // 下入力
                else if (Input.GetKeyDown(KeyCode.S) && Listnum < box - 1)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    Listnum++; 
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
                    // マスター
                    if (Listnum == 0)
                    {
                        method = 1;
                    }
                    // BGM
                    else if (Listnum == 1)
                    {
                        method = 2;
                    }
                    // SE
                    else if (Listnum == 2)
                    {
                        method = 3;
                    }
                    else if(Listnum==3) // 完了ボタン
                    {
                        obj.GetComponent<C_Audio>().enabled = false;
                        this.gameObject.GetComponent<Image>().enabled = false;
                        panel.SetActive(true);
                        ConfigP.GetComponent<Config>().enabled = true;

                        setvol();

                        Listnum = 0;
                        Master_Num = 0;
                        BGM_Num = 0;
                        SE_Num = 0;
                        method = 0;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.tab);
                    obj.GetComponent<C_Audio>().enabled = false;
                    this.gameObject.GetComponent<Image>().enabled = false;
                    panel.SetActive(true);
                    ConfigP.GetComponent<Config>().enabled = true;


                    Listnum = 0;
                    Master_Num = 0;
                    BGM_Num = 0;
                    SE_Num = 0;
                    method = 0;
                    return;
                }
                break;
            case 1:
                // 左入力
                if (Input.GetKeyDown(KeyCode.A)&&Master_Num>0 )
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    Master_Num--;
                }
                // 右入力
                else if (Input.GetKeyDown(KeyCode.D)&&Master_Num<box-1 )
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    Master_Num++;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
                    if (Master_Now_Num!=10&&Master_Num==1)
                    {
                        Master_Now_Num++;
                    }
                    else if(Master_Now_Num!=0&&Master_Num==0)
                    {
                        Master_Now_Num--;
                    }
                    //method = 0;
                }
                break;
            case 2:
                // 左入力
                if (Input.GetKeyDown(KeyCode.A) && BGM_Num > 0)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    BGM_Num--;
                }
                // 右入力
                else if (Input.GetKeyDown(KeyCode.D) && BGM_Num < box - 1)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    BGM_Num++;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
                    if (BGM_Now_Num != 10 && BGM_Num == 1)
                    {
                        BGM_Now_Num++;
                    }
                    else if (BGM_Now_Num != 0 && BGM_Num == 0)
                    {
                        BGM_Now_Num--;
                    }
                    //method = 0;
                }
                break;
            case 3:
                // 左入力
                if (Input.GetKeyDown(KeyCode.A) && SE_Num > 0)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    SE_Num--;
                }
                // 右入力
                else if (Input.GetKeyDown(KeyCode.D) && SE_Num < box - 1)
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.wasd);
                    SE_Num++;
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.enter);
                    if (SE_Now_Num != 10 && SE_Num == 1)
                    {
                        SE_Now_Num++;
                    }
                    else if (SE_Now_Num != 0 && SE_Num == 0)
                    {
                        SE_Now_Num--;
                    }
                    //method = 0;
                }
                break;
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.tab);
            Master_Num = 0;
            BGM_Num = 0;
            SE_Num = 0;
            method = 0;
            _Master.SetActive(false);
            _BGM.SetActive(false);
            _SE.SetActive(false);
        }
    }
    
    void _pos(RectTransform[] box,int num)
    {
        x = box[num].anchoredPosition.x;
        y = box[num].anchoredPosition.y;
        obj.anchoredPosition = new Vector2(x, y);
        if (box != List)
        {
            obj.sizeDelta = new Vector2(box[num].rect.width, box[num].rect.height);
        }
        else
        {
            // 初期サイズ
            obj.sizeDelta = new Vector2(wid, hei);
        }
    }

    // バーの表示設定に関する関数
    // Imageは見た目
    // Textは現在の値を表示させる
    // intは配列の番号
    void image(Image[] img,Text text,int now)
    {
        for(int i=img.Length;i>now;i--)
        {
            img[i-1].enabled = false;
        }
        for(int i = 0; i < now; i++)
        {
            img[i].enabled = true;
        }
        text.text = now.ToString();
    }

    void setvol()
    {
        Copy_Master_Now_Num = Master_Now_Num;
        Copy_BGM_Now_Num = BGM_Now_Num;
        Copy_SE_Now_Num = SE_Now_Num;

        para.Para_Master = Master_Now_Num * 0.1f;
        para.Para_Bgm    = BGM_Now_Num * 0.1f;
        para.Para_Se     = SE_Now_Num * 0.1f;

    }
}
