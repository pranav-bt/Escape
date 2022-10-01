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
    [SerializeField] private float JumpPowerUpDuration = 5.0f;
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
    float JumpCoolDownDuration = 1.2f;
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
    [SerializeField] public AudioClip JumpPowerUpAudio;
    [SerializeField] public AudioClip TeleportAudio;
    private IEnumerator BlinkCoroutine;
    // Start is called before the first frame update


    private void OnEnable()
    {
        EventManager.DamageEvent += DamagePlayer;
        EventManager.GameOver += GameOver;
        EventManager.CharacterSwitch += CharacterSwitch;
        EventManager.CoinCollected += UpdateCoinText;
        EventManager.PlayerWin += PlayerWin;
    }

    private void OnDisable()
    {
        EventManager.DamageEvent -= DamagePlayer;
        EventManager.GameOver -= GameOver;
        EventManager.CharacterSwitch -= CharacterSwitch;
        EventManager.CoinCollected -= UpdateCoinText;
        EventManager.PlayerWin -= PlayerWin;
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
        IPcontroller.DiegeticCoinText.gameObject.SetActive(true);
    }

    // Update is called once per frame
    bool Once = false;
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
            if(!Once)
            {
                BlinkCoroutine = BlinkGameObject(gameObject, 30, JumpPowerUpDuration);
                StartCoroutine(BlinkCoroutine);
                Once = true;
            }
            PowerUpCooldown += Time.deltaTime;
            if (PowerUpCooldown > JumpPowerUpDuration)
            {
                PowerUpBoost = 1.0f;
                PowerUpCooldown = 0.0f;
                StopCoroutine(BlinkCoroutine);
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
                Once = false;
            }
            if (!bCanJump)
            {
                JumpCooldown += Time.deltaTime;
                if (JumpCooldown > 1.5f)
                {
                    bCanJump = true;
                    NextPlayerState = PlayerState.Idle;
                }
            }
        }

        if(PowerUpBoost <= 1.0f && !bCanJump)
        {
            JumpCooldown += Time.deltaTime;
            if(JumpCooldown > JumpCoolDownDuration)
            {
                bCanJump = true;
                NextPlayerState = PlayerState.Idle;
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
        if(other.gameObject.tag == "Collectible")
        {
            Destroy(other.gameObject);
            CollectiblesCollected++;
            //Audioplayer.PlayOneShot(CollectibleAudio);
            if (CollectiblesCollected >= TotalCollectiblesInLevel)
            {
                WinConditionsMet = true;
            }
            EventManager.BroadCastCoinCollectedEvent();
            return;
        }
        else if (other.gameObject.tag == "Hazard")
        {
            EventManager.BroadCastDamageEvent();
            return;
        }
        else if (other.gameObject.tag == "GameOver")
        {
            // broadcast event
            EventManager.BroadCastGameOverEvent();
            return;
        }
    }

    private void GameOver()
    {
        //Audioplayer.PlayOneShot(GameOverAudio);
        WinLoseCanvas.SetActive(true);
        WinLoseCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Game Over";
        ReadInput = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerSwitch")
        {
            EventManager.BroadCastCharacterSwitchEvent();
            return;
        }
    }

    private void DamagePlayer()
    {
        if(CurrentHealthPart >= MaxHealthParts)
        {
            EventManager.BroadCastGameOverEvent();
        }
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
        if (BlinkCoroutine != null)
        { StopCoroutine(BlinkCoroutine); gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true; }
    }


    public IEnumerator BlinkGameObject(GameObject gameObject, int numBlinks, float seconds)
    {
        int j = 0; int i = 0;
        SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        for (i = 0; i < numBlinks * 2; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(0.2f);
        }
    }


    private void PlayerWin()
    {
        WinLoseCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "Level Completed!";
        WinLoseCanvas.SetActive(true);
        //Audioplayer.PlayOneShot(PlayerWinAudio);
        ReadInput = false;
    }
}
