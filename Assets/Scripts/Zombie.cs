using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Component References
    public Animator ZombieAnimator;
    public Rigidbody2D ZombieRigidBody;
    public AIStateManager stateManager;

    //variables
    public float Health = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        ZombieAnimator = GetComponent<Animator>();
        ZombieRigidBody = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<AIStateManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        Health -= 10.0f;
        if(Health <= 0 && stateManager.IsAIAlive && !stateManager.once)
        {
            stateManager.ChangeState(stateManager.DeathState);
        }
    }
}
