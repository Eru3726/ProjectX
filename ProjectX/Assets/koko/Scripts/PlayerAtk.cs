using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerPunchPrefab;

    public int atkDir;

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.P))
        {
            Vector3 pos = this.transform.position;
            pos.x += atkDir;

            Instantiate(PlayerPunchPrefab, pos, Quaternion.identity, this.transform);
        }
    }


}
