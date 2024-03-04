using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmDieEffect : MonoBehaviour
{
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
