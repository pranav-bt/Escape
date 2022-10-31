using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum KnightInputKey
{
    Left,
    Right,
    Up,
    Down,
    Jump,
    Attack,
    Dash,
    Strike
}
public class InputControllerKnight : MonoBehaviour
{
    [HideInInspector] public KnightInputKey CurrentInput;
    [HideInInspector] public Knight Player;
    [HideInInspector] public StateManager KnightStateManager;
    // Update is called once per frame
    private void Start()
    { 
        KnightStateManager = FindObjectOfType<StateManager>();
        Player = FindObjectOfType<Knight>();
    }
    void Update()
    {
        if (Player.CanMove) 
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                CurrentInput = KnightInputKey.Left;
                KnightStateManager.ChangeState(KnightStateManager.WalkState);
                
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                CurrentInput = KnightInputKey.Right;
                KnightStateManager.ChangeState(KnightStateManager.WalkState);
                
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                CurrentInput = KnightInputKey.Jump;
                KnightStateManager.ChangeState(KnightStateManager.JumpState);
            }
            else if (Input.GetKey(KeyCode.LeftAlt))
            {
                CurrentInput = KnightInputKey.Dash;
                KnightStateManager.ChangeState(KnightStateManager.DashState);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                CurrentInput = KnightInputKey.Attack;
                KnightStateManager.ChangeState(KnightStateManager.AttackState);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                CurrentInput = KnightInputKey.Strike;
                KnightStateManager.ChangeState(KnightStateManager.StrikeState);
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                FindObjectOfType<Knight>().ActivateTeleportCheat(1);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                FindObjectOfType<Knight>().ActivateTeleportCheat(2);
            }
        }
        if (Input.GetKey(KeyCode.R) && Player.LevelComplete)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.Escape) && Player.LevelComplete)
        {
            Application.Quit();
        }
    }
}
