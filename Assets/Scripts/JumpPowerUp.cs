using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerLogic>().ActivePlayer == CurrentPlayer.Square1)
        {
            var PL = collision.gameObject.GetComponent<PlayerLogic>();
            PL.PowerUpBoost = 1.5f;
            PL.Audioplayer.PlayOneShot(PL.JumpPowerUpAudio);
        }
    }
}
