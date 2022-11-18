using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    //Component References
    public Animator ZombieAnimator;
    public Rigidbody2D ZombieRigidBody;
    public AIStateManager stateManager;
    public EnemyHealthBar HealthComponent;

    //variables
    public float Health = 100.0f;
    public float DamageTaken = 0.0f;

    //VFX
    public GameObject HitVFX;

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
        DamageTaken += 20.0f;
        Health -= 20.0f;
        HealthComponent.UpdateHealthBar(Health);
        Instantiate(HitVFX, transform);
        if (DamageTaken >= 80)
        { 
            GetComponent<SpriteRenderer>().color = new Color(255 * (DamageTaken / 100), 0.0f, 0.0f); 
        }
        if (Health <= 0 && stateManager.IsAIAlive && !stateManager.once)
        {
            stateManager.ChangeState(stateManager.DeathState);
        }
    }

    public void ProjectileDamage()
    {
        DamageTaken += 40.0f;
        Health -= 40.0f;
        HealthComponent.UpdateHealthBar(Health);
        if (DamageTaken >= 80)
        {
            GetComponent<SpriteRenderer>().color = new Color(255 * (DamageTaken / 100), 0.0f, 0.0f);
        }
        if (Health <= 0 && stateManager.IsAIAlive && !stateManager.once)
        {
            stateManager.ChangeState(stateManager.DeathState);
        }
    }

}
