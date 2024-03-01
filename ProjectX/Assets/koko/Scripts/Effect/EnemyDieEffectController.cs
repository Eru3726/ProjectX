using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieEffectController : MonoBehaviour
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
