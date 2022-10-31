using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GetComponentInParent<Zombie>().stateManager.PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponentInParent<Zombie>().stateManager.PlayerInRange = false;   
    }
}
