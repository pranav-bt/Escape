using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeadState : State
{
    public Enemy_DeadState(Zombie owner) { Owner = owner; }
    public override void BeginState()
    {
        
        Owner.ZombieAnimator.SetBool("Walking", false);
        Owner.ZombieAnimator.SetBool("Attacking", false);
        Owner.ZombieAnimator.SetBool("Dead", true);
        Owner.stateManager.IsAIAlive = false;
    }

    public override void EndState()
    {
        
    }

    public override void Update()
    {
       
    }


}
