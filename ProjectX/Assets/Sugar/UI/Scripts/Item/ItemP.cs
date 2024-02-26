using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemP : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] Mng_Game Manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Manager.OneShotSE_U(SEData.Type.UISE, Mng_Game.UISe.tab);
            Menu.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
