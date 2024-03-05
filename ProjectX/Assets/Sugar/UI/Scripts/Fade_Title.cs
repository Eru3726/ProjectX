using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade_Title : MonoBehaviour
{
    [SerializeField] bool Str = false;
    [SerializeField] bool End = false;
    Color col;
    void Start()
    {
        col = this.gameObject.GetComponent<Image>().color;
    }
    private void OnEnable()
    {
        col.a = 0;
        // アルファ0に設定
        this.gameObject.GetComponent<Image>().color = col;
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (col.a <= 1)
        {
            col.a += 0.1f;
            this.gameObject.GetComponent<Image>().color = col;
        }
        else
        {
            if (Str) 
            {
                SceneManager.LoadScene("Heaven1");
            }
            else if (End) 
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif

            }
            //this.gameObject.SetActive(false);
        }
    }
}
