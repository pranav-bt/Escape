using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    // Start is called before the first frame update
    public JumpState(Knight player) { Player = player; }
    public bool bCanJump = true;
    
    public override void BeginState()
    {
        if (/*bCanJump &&*/ Player.bJumpAvailable)
        {
            bCanJump = false;
            Player.JumpCooldown = 1.6f;
            Player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3.0f * Player.PowerUpBoost, ForceMode2D.Impulse);
            Player.bJumpAvailable = false;
        }
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void EndState()
    {

    }
}
