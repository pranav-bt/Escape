using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    // Start is called before the first frame update
    public WalkState(Knight player) { Player = player; }
    public override void BeginState() 
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        float horval = Input.GetAxis("Horizontal");       
        Vector2 val = new Vector2(horval * 3.0f, Player.KnightRigidBody.velocity.y);
        Player.KnightRigidBody.velocity = val;
        spriteflip(horval);
        bool playermotion = Mathf.Abs(horval) > 0;
        Player.KnightAnimator.SetBool("Running", playermotion);
    }

    public override void EndState() 
    {

    }

    private void spriteflip(float horval)
    {
        Transform sp = Player.GetComponent<Transform>();
        if (horval > 0)
        {
            sp.localScale = new Vector2(0.7f, 0.7f);
        }
        if (horval < 0)
        {
            sp.localScale = new Vector2(-0.7f, 0.7f);
        }
    }
}
