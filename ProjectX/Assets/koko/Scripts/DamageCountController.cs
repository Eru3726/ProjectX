using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCountController : MonoBehaviour
{
    Text countText;

    [SerializeField, Header("消えるまでの時間")]
    float destroyTime = 1;

    float timer;

    private void Start()
    {
        countText = GetComponent<Text>();
        countText.color = new Color32(255, 0, 0, 255);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        float a = 255 - (timer * 255 * destroyTime);
        countText.color = new Color32(255, 0, 0, (byte)a);

        Vector3 pos = transform.position;
        pos.y += Time.deltaTime;
        transform.position = pos;

        if (timer > destroyTime)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
