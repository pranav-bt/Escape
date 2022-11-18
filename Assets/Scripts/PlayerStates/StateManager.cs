using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public State IdleState;
    public State WalkState;
    public State AttackState;
    public State DeathState;
    public State JumpState;
    public State DashState;
    public State StrikeState;
    public State CastState;

    State CurrentState;
    void Start()
    {
        Knight player = FindObjectOfType<Knight>();
        IdleState = new IdleState(player);
        WalkState = new WalkState(player);
        AttackState = new AttackState(player);
        DeathState = new DeathState(player);
        JumpState = new JumpState(player);
        DashState = new DashState(player, false);
        StrikeState = new StrikeState(player, false);
        CastState = new CastState(player);
        CurrentState = IdleState;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != null)
        { CurrentState.Update(); }
    }

    public void ChangeState(State NewState)
    {
        if (NewState.StateUnlocked)
        {
            CurrentState.EndState();

            CurrentState = NewState;

            CurrentState.BeginState();
        }
    }

    public void IdleReturn()
    {
        ChangeState(IdleState);
    }

    public void EndDeath()
    {
        FindObjectOfType<Knight>().KnightAnimator.SetBool("Death", false);
    }

   
}
