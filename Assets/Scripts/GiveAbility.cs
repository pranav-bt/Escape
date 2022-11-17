using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAbility : MonoBehaviour
{
    public int UnlockIndex;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<Knight>().UnlockAbility(UnlockIndex);
            FindObjectOfType<AudioPlayer>().PlayOneShot(other.GetComponent<Knight>().SFX_Ability); 
            Destroy(gameObject);
        }
    }
}
