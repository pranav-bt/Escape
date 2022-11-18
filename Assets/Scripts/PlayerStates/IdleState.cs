using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    // Start is called before the first frame update
    public IdleState(Knight player) { Player = player; }
    public override void BeginState()
    {
        Player.KnightRigidBody.velocity = new Vector2(0, Player.KnightRigidBody.velocity.y);
        Player.KnightAnimator.SetBool("Running", false);
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void EndState()
    {

    }
}
