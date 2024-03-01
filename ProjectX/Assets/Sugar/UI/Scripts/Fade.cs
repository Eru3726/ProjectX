using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    [Header("項目のオブジェクトを入れる")]
    [SerializeField] GameObject Item;
    [SerializeField] GameObject Menu;

    // アウトラインの座標を戻すように
    [SerializeField] RectTransform rtf;
    Color col;
    private void Start()
    {
       col  =  this.gameObject.GetComponent<Image>().color;
    }
    private void OnEnable()
    {
        col.a= 0;
        // アルファ0に設定
        this.gameObject.GetComponent<Image>().color = col;
    }
    // Update is called once per frame
    void Update()
    {
        if(col.a<=1)
        {
            col.a += 0.1f;
            this.gameObject.GetComponent<Image>().color = col;
        }
        else 
        {
            rtf.localRotation = Quaternion.Euler(0, 0, 0);
            Menu.SetActive(false);
            Item.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
