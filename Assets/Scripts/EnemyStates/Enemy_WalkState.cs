using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WalkState : State
{
    public Enemy_WalkState(Zombie owner) { Owner = owner; }
    float WalkStateTimer = 5.0f;
    public override void BeginState()
    {
        Owner.ZombieAnimator.SetBool("Walking", true);
    }

    public override void EndState()
    {
        Owner.ZombieAnimator.SetBool("Walking", false);
    }

    public override void Update()
    {
        Vector2 location = new Vector2(GameObject.FindObjectOfType<Knight>().transform.position.x, GameObject.FindObjectOfType<Knight>().transform.position.y) ;
        if (Owner.gameObject.transform.position.x - location.x < Mathf.Epsilon)
        {
            Owner.gameObject.transform.localScale = new Vector2(1, 1);
        }
        if (Owner.gameObject.transform.position.x - location.x > Mathf.Epsilon)
        {
            Owner.gameObject.transform.localScale = new Vector2(-1, 1);
        }
        Vector2 finallocation = new Vector2(location.x, Owner.gameObject.transform.position.y);
        Owner.gameObject.transform.position = Vector2.MoveTowards(new Vector2(Owner.gameObject.transform.position.x, Owner.gameObject.transform.position.y), finallocation, 1 * Time.deltaTime);
        WalkStateTimer -= Time.deltaTime;
        
        if(WalkStateTimer <= 0.0f)
        {
            Owner.stateManager.ChangeState(Owner.stateManager.IdleState);
        }
    }


}
