using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackState : State
{
    public Enemy_AttackState(Zombie owner) { Owner = owner; }
    public override void BeginState()
    {
        Owner.ZombieAnimator.SetBool("Attacking", true);
        GameObject.FindObjectOfType<Knight>().DamageHealth();
    }

    public override void EndState()
    {
        Owner.ZombieAnimator.SetBool("Attacking", false);
    }

    public override void Update()
    {
        
    }

}
