using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    // Start is called before the first frame update
    public DeathState(Knight player) { Player = player; }
    public override void BeginState()
    {
        Player.KnightAnimator.SetBool("Death", true);
        Player.CanMove = false;
        Player.LevelComplete = true;
        GameObject.FindObjectOfType<LevelEnd>().Text.text = "GAME OVER \n Press R to Restart or Esc to Quit";
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void EndState()
    {

    }
}
