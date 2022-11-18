using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Knight : MonoBehaviour
{
    // Start is called before the first frame update

    //Component References
    public Animator KnightAnimator;
    public Rigidbody2D KnightRigidBody;
    public StateManager stateManager;

    //Effects
    public GameObject VFX_Dash;
    public GameObject Projectile;
    public AudioClip SFX_Strike;
    public AudioClip SFX_Damage;
    public AudioClip SFX_Dash;
    public AudioClip SFX_Ability;
    public AudioClip SFX_Switch;

    //Jump
    public float PowerUpBoost = 1.0f;
    public float JumpCooldown = 1.0f;
    public bool bJumpAvailable = false;

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
    public bool FacingRight = true;
    public float DamageTimer = 1.0f;
    public float CastCooldown = 5.0f;
    public float CastTimer = 0.0f;
    public bool StartCastTimer = false;
    public GameObject ProjectileSpawnLocation;
    public GameObject ProjectileCooldownUI;

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
/*        JumpState JS = stateManager.JumpState as JumpState;
        if (JS != null && !JS.bCanJump)
        {
            JumpCooldown -= Time.deltaTime;
            if (JumpCooldown < 0.0f)
            {
                JS.bCanJump = true;
                JumpCooldown = 1.6f;
                stateManager.ChangeState(stateManager.IdleState);
            }
        }*/
        if (StartCastTimer)
        {
            CastTimer += Time.deltaTime;
            if(CastTimer > CastCooldown)
            {
                stateManager.CastState.StateUnlocked = true;
                ProjectileCooldownUI.GetComponent<Image>().color = new Color(255, 255, 255);
                StartCastTimer = false;
                CastTimer = 0.0f;
            }
        }
        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.right , 5f);
        //Debug.DrawRay(transform.position, Vector2.right*12f, Color.green);
        if (Hit && Hit.collider.gameObject.tag == "Enemy")
        {
            Hit.collider.gameObject.GetComponent<Zombie>().stateManager.PlayerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        DamageTimer -= Time.deltaTime;
        if (IsAttacking && other.gameObject.tag == "Enemy" && DamageTimer <= 0.0f)
        {
            if (other.gameObject.GetComponent<Zombie>().Health > 0)
            {
                FindObjectOfType<AudioPlayer>().PlayOneShot(SFX_Damage);
                other.GetComponent<Zombie>().TakeDamage();
                DamageTimer = 1.0f;
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
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            bJumpAvailable = true;
        }
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
        Health -= 0.5f; 
        EventManager.BroadCastDamageEvent();
        }
        if (Health <= 0.0f && CanMove)
        {
            stateManager.ChangeState(stateManager.DeathState);
            EventManager.BroadCastDamageEvent();
        }
        UpdateUI();
    }

    public void CastSpell()
    {
        Vector3 Location = new Vector3(transform.position.x, transform.position.y, -8);
        GameObject Proj = Instantiate(Projectile, Location, transform.rotation);
        if(FacingRight)
        {
            Proj.GetComponent<Projectile>().Direction = 1.0f;
        }
        else 
        {
            Proj.GetComponent<Projectile>().Direction = -1.0f;
        }
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
