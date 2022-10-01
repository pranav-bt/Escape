using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerLogic PL = collision.gameObject.GetComponent<PlayerLogic>();
            if (PL.WinConditionsMet)
            {
                EventManager.BroadCastPlayerWinEvent();
            }
        }
    }
}
