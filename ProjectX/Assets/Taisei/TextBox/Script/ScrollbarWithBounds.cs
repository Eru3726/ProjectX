using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarWithBounds : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;
    public float scrollSpeed = 0.1f;
    public float minValue = 0f;
    public float maxValue = 1f;
    private float counter = 0;
    float verticalInput;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetAxis("Vertical"));
        counter++;
        // キーボードの上下矢印キーを使用してスクロールバーを操作する
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            verticalInput = 1;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            verticalInput = -1;
        }
        
        scrollbar.value += verticalInput * scrollSpeed * (counter / 60);

        // スクロールバーの値を範囲内に制限する
        scrollbar.value = Mathf.Clamp(scrollbar.value, minValue, maxValue);
    }
}
