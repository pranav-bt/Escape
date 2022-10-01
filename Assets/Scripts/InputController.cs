using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum InputKey
{
    Left,
    Right,
    Up,
    Down,
    Jump
}
public class InputController : MonoBehaviour
{
    [HideInInspector] public InputKey CurrentInput;
    [HideInInspector] public PlayerLogic Player;
    [SerializeField] public GameObject DiegeticCoinText;
    // Update is called once per frame
    private void Start()
    {
        Player = FindObjectOfType<PlayerLogic>();    
    }
    void Update()
    {
        if (Player.ReadInput)
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("LevelSelection");
            }
            if (Input.GetKey(KeyCode.Tab))
            {
                DiegeticCoinText.SetActive(true);
            }    
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                CurrentInput = InputKey.Left;
                Player.NextPlayerState = PlayerState.PlayerMovement;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CurrentInput = InputKey.Right;
                Player.NextPlayerState = PlayerState.PlayerMovement;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                CurrentInput = InputKey.Up;
                Player.NextPlayerState = PlayerState.PlayerMovement;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                CurrentInput = InputKey.Down;
                Player.NextPlayerState = PlayerState.PlayerMovement;
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                CurrentInput = InputKey.Jump;
                Player.NextPlayerState = PlayerState.Jump;
            }
            else
            {
                if (Player.CurrentPlayerState != PlayerState.Jump)
                    Player.NextPlayerState = PlayerState.Idle;
            }
        }
    }
}
