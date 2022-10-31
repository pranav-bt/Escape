using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knight : MonoBehaviour
{
    // Start is called before the first frame update

    //Component References
    public Animator KnightAnimator;
    public Rigidbody2D KnightRigidBody;
    public StateManager stateManager;

    //Effects
    public GameObject VFX_Dash;
    public AudioClip SFX_Strike;
    public AudioClip SFX_Damage;

    //Jump
    public float PowerUpBoost = 1.0f;
    public float JumpCooldown = 1.2f;

    //Attack and Abilities
    public bool IsAttacking = false;
    public bool IsStriking = false;
    public bool IsDashing = false;

    // Player Variables
    public float Health = 100.0f;
    public int CoinsCollected = 0;
    public int CoinsInLevel = 0;
    public bool CanMove = true;
    public bool LevelComplete = false;

    //UI references
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI HealthText;

    //CheatCode References
    public GameObject Point1;
    public GameObject Point2;
    void Start()
    {
        KnightAnimator = GetComponent<Animator>();
        KnightRigidBody = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<StateManager>();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        CoinsInLevel = GameObject.FindGameObjectsWithTag("Collectible").Length;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        JumpState JS = stateManager.JumpState as JumpState;
        if (JS != null && !JS.bCanJump)
        {
            JumpCooldown -= Time.deltaTime;
            if (JumpCooldown < 0.0f)
            {
                JS.bCanJump = true;
                JumpCooldown = 1.6f;
                stateManager.ChangeState(stateManager.IdleState);
            }
        }

        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.right , 15f);
        //Debug.DrawRay(transform.position, Vector2.right*12f, Color.green);
        if (Hit && Hit.collider.gameObject.tag == "Enemy")
        {
            Hit.collider.gameObject.GetComponent<Zombie>().stateManager.PlayerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsAttacking && other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<Zombie>().Health > 0)
            {
                FindObjectOfType<AudioPlayer>().PlayOneShot(SFX_Damage);
                other.GetComponent<Zombie>().TakeDamage();
            }
        }
        if (IsStriking && other.gameObject.tag == "WoodBlock")
        {
            Destroy(other.gameObject);
        }
        if (IsDashing && other.gameObject.tag == "Wood")
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Collectible")
        {
            CoinsCollected++;
            Destroy(other.gameObject);
            EventManager.BroadCastCoinCollectedEvent();
            UpdateUI();
        }
    }

    public void EnableAttackDetection()
    {
        IsAttacking = true;
    }

    public void DamageHealth()
    {
        if (Health > 0.0f)
        { 
        Health -= 2.0f; 
        EventManager.BroadCastDamageEvent();
        }
        if (Health <= 0.0f && CanMove)
        {
            stateManager.ChangeState(stateManager.DeathState);
            EventManager.BroadCastDamageEvent();
        }
        UpdateUI();
    }

    public void UnlockAbility(int Index)
    {
        switch (Index)
        { 
            case 0:
                DashState dash = stateManager.DashState as DashState;
                dash.UnlockDash();
                break;
            case 1:
                StrikeState strike = stateManager.StrikeState as StrikeState;
                strike.UnlockStrike();
                break;
            default:
                break;
        }
    }

    public void UpdateUI()
    {
        CoinText.text = $"Coins - {CoinsCollected} / {CoinsInLevel}";
        HealthText.text = $"Health - {Health}";
    }

    public void ActivateTeleportCheat(int Index)
    {
        if(Index==1)
        {
            gameObject.transform.position = new Vector2(Point1.gameObject.transform.position.x, Point1.gameObject.transform.position.y);
        }
        if (Index == 2)
        {
            gameObject.transform.position = new Vector2(Point2.gameObject.transform.position.x, Point2.gameObject.transform.position.y);
        }
    }
}
