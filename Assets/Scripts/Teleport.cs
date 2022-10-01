using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject TeleportToLocation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerLogic>().ActivePlayer == CurrentPlayer.Square2)
        {
            var NewPosition = TeleportToLocation.gameObject.transform.position;
            collision.gameObject.transform.position = new Vector3(NewPosition.x,NewPosition.y,NewPosition.z);
            var PL = collision.gameObject.GetComponent<PlayerLogic>();
            PL.Audioplayer.PlayOneShot(PL.TeleportAudio);
        }
    }
}
