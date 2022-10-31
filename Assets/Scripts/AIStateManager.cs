using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public State IdleState;
    public State WalkState;
    public State AttackState;
    public State DeathState;
    public State CurrentState;

    //booleans to control the AI
    public bool PlayerInRange = false;
    public bool IsAIAlive = true;
    public bool once = false;

    void Start()
    {
        IdleState = new Enemy_IdleState(gameObject.GetComponent<Zombie>());
        WalkState = new Enemy_WalkState(gameObject.GetComponent<Zombie>());
        AttackState = new Enemy_AttackState(gameObject.GetComponent<Zombie>());
        DeathState = new Enemy_DeadState(gameObject.GetComponent<Zombie>());
        CurrentState = IdleState;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState != null && IsAIAlive)
        { CurrentState.Update(); }

        ChangeStateConditions();
    }
    public void ChangeState(State NewState)
    {
        CurrentState.EndState();

        CurrentState = NewState;

        CurrentState.BeginState();
    }

    private void ChangeStateConditions()
    {
        if (PlayerInRange && CurrentState != AttackState && IsAIAlive)
        {
            ChangeState(WalkState);
        }
        if (!IsAIAlive)
        {
            ChangeState(DeathState);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && IsAIAlive)
        {
            ChangeState(AttackState);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsAIAlive)
        { 
            ChangeState(WalkState); 
        }
    }

    public void EndDeath()
    {
        if(!once)
            FindObjectOfType<Zombie>().ZombieAnimator.SetBool("Dead", false);
    }

    public void DeleteActor()
    {
        Destroy(gameObject);
    }
}
