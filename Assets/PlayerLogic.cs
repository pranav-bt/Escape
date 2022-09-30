using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public enum PlayerState
{
    Idle,
    PlayerMovement,
    Jump
}

public enum CurrentPlayer
{
    Square1,
    Square2
}


public class PlayerLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> HealthParts;
    private Dictionary<GameObject, bool> HealthBar;
    public CurrentPlayer ActivePlayer = CurrentPlayer.Square1;
    public PlayerState CurrentPlayerState = PlayerState.Idle;
    private PlayerState PreviousPlayerState;
    public PlayerState NextPlayerState;
    private InputController IPcontroller;
    private InputKey CurrentInput;
    public float moveSpeed = 2.0f;
    public float JumpVelocity = 300.0f;
    public float PowerUpBoost = 1.0f;
    private float PowerUpCooldown = 0.0f;
    float JumpCooldown = 0.0f;
    float JumpCoolDownDuration = 1.5f;
    bool bCanJump = true;
    [HideInInspector] public bool WinConditionsMet = false;
    private int TotalCollectiblesInLevel;
    private int CollectiblesCollected = 0;
    private int MaxHealthParts = 0;
    private int CurrentHealthPart = 0;
    public GameObject WinLoseCanvas;
    public TextMeshProUGUI CoinText;
    [HideInInspector] public bool ReadInput = true;
    [SerializeField] public AudioPlayer Audioplayer;
    [SerializeField] private AudioClip CharacterSwitchAudio;
    [SerializeField] private AudioClip CollectibleAudio;
    [SerializeField] private AudioClip GameOverAudio;
    [SerializeField] public AudioClip PlayerWinAudio;
    [SerializeField] public AudioClip JumpPowerUpAudio;
    [SerializeField] public AudioClip TeleportAudio;
    // Start is called before the first frame update


    private void OnEnable()
    {
        EventManager.DamageEvent += DamagePlayer;
    }

    private void OnDisable()
    {
        EventManager.DamageEvent -= DamagePlayer;
    }

    void Start()
    {
        IPcontroller = FindObjectOfType<InputController>();
        TotalCollectiblesInLevel = GameObject.FindGameObjectsWithTag("Collectible").Length;
        UpdateCoinText();
        ReadInput = true;
        HealthBar = new Dictionary<GameObject, bool>();
        foreach(GameObject HealthPart in HealthParts)
        {
            MaxHealthParts++;
            HealthBar.Add(HealthPart, true);
        }
    }

    private void UpdateCoinText()
    {
        CoinText.text = $"{CollectiblesCollected} / {TotalCollectiblesInLevel}";
    }

    // Update is called once per frame
    void Update()
    {
        CurrentInput = IPcontroller.CurrentInput;

        if(CurrentPlayerState != NextPlayerState)
        {
            ChangeState();
        }

        UpdateState();

        if(PowerUpBoost>1.0f)
        {
            PowerUpCooldown += Time.deltaTime;
            if (PowerUpCooldown > 5.0f)
            {
                PowerUpBoost = 1.0f;
                PowerUpCooldown = 0.0f;
            }
        }

        if(!bCanJump)
        {
            JumpCooldown += Time.deltaTime;
            if(JumpCooldown > JumpCoolDownDuration)
            {
                bCanJump = true;
            }
        }

    }

    private void ChangeState()
    {
        switch (CurrentPlayerState)
        {
            case PlayerState.PlayerMovement:
                MovementEnd();
                break;
            case PlayerState.Idle:
                IdleEnd();
                break;
            case PlayerState.Jump:
                JumpEnd();
                break;
        }

        CurrentPlayerState = NextPlayerState;

        switch (CurrentPlayerState)
        {
            case PlayerState.PlayerMovement:
                MovementBegin();
                break;
            case PlayerState.Idle:
                IdleBegin();
                break;
            case PlayerState.Jump:
                JumpBegin();
                break;
        }
    }

    private void UpdateState()
    {
        switch (CurrentPlayerState)
        {
            case PlayerState.PlayerMovement:
                MovementUpdate();
                break;
            case PlayerState.Idle:
                IdleUpdate();
                break;
            case PlayerState.Jump:
                JumpUpdate();
                break;
        }
    }

    void MovementBegin()
    {

    }

    void MovementUpdate()
    {
        switch (CurrentInput)
        {
            case InputKey.Left:
                MoveLeft();
                break;
            case InputKey.Right:
                MoveRight();
                break;
        }
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    private void MoveLeft()
    {
        transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
    }

    void MovementEnd()
    {

    }

    void JumpBegin()
    {
        if (bCanJump)
        {
            bCanJump = false;
            JumpCooldown = 0.0f;
            if (ActivePlayer == CurrentPlayer.Square1)
            { GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpVelocity * PowerUpBoost, ForceMode2D.Impulse); }
            else
            { GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpVelocity, ForceMode2D.Impulse); }
        }
    }

    void JumpUpdate()
    {
        
    }
    void JumpEnd()
    {

    }

    void IdleEnd()
    {

    }

    void IdleBegin()
    {

    }

    void IdleUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "PlayerSwitch")
        {
            CharacterSwitch();
            return;
        }
        else if(other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            CollectiblesCollected++;
            Audioplayer.PlayOneShot(CollectibleAudio);
            UpdateCoinText();
            if (CollectiblesCollected >= TotalCollectiblesInLevel)
            {
                WinConditionsMet = true;
            }
            return;
        }
        else if (other.gameObject.tag == "Hazard")
        {
            EventManager.BroadCastDamageEvent();
            return;
        }
        else if (other.gameObject.tag == "GameOver")
        {
            Audioplayer.PlayOneShot(GameOverAudio);
            WinLoseCanvas.SetActive(true);
            WinLoseCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";
            ReadInput = false;
            return;
        }
    }

    private void DamagePlayer()
    {
        foreach (KeyValuePair<GameObject, bool> HealthPart in HealthBar)
        {
            if (CurrentHealthPart < MaxHealthParts && HealthPart.Key == HealthParts[CurrentHealthPart])
            {
                HealthBar[HealthPart.Key] = false;
                HealthPart.Key.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                CurrentHealthPart++;
                return;
            }
        }

    }

    private void CharacterSwitch()
    {
        switch(ActivePlayer)
        {
            case CurrentPlayer.Square1:
                ActivePlayer = CurrentPlayer.Square2;
                foreach(KeyValuePair<GameObject, bool> HealthPart in HealthBar)
                {
                    if (HealthPart.Value == true)
                    { HealthPart.Key.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0); }
                }
                break;
            case CurrentPlayer.Square2:
                ActivePlayer = CurrentPlayer.Square1;
                foreach (KeyValuePair<GameObject, bool> HealthPart in HealthBar)
                {
                    if (HealthPart.Value == true)
                    { HealthPart.Key.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0); }
                }
                break;
        }
        Audioplayer.PlayOneShot(CharacterSwitchAudio);
    }
}
