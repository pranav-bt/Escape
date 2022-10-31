using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    // Start is called before the first frame update
    public AttackState(Knight player) { Player = player; }
    public override void BeginState()
    {
        Player.KnightAnimator.SetBool("Attack", true);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void EndState()
    {
        Player.KnightAnimator.SetBool("Attack", false);
        Player.IsAttacking = false;
    }
}
