using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEnd : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Text.text = "LEVEL COMPLETE \n Press R to Restart or Esc to Quit";
            EventManager.BroadCastPlayerWinEvent();
            collision.gameObject.GetComponent<Knight>().LevelComplete = true; 
            collision.gameObject.GetComponent<Knight>().CanMove = false; 
        }
    }
}
