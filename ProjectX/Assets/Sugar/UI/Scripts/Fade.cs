using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    [Header("項目のオブジェクトを入れる")]
    [SerializeField] GameObject Item;
    [SerializeField] GameObject Menu;
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
            col.a += 0.05f;
            this.gameObject.GetComponent<Image>().color = col;
        }
        else 
        {
            Menu.SetActive(false);
            Item.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
