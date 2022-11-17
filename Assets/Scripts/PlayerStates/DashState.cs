using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    // Start is called before the first frame update
    public DashState(Knight player) { Player = player; }

    public DashState(Knight player, bool Unlocked) { Player = player; StateUnlocked = Unlocked; }

    
    public override void BeginState()
    {
        Player.KnightAnimator.SetBool("Dash", true);
        Player.IsDashing = true;
        GameObject.FindObjectOfType<AudioPlayer>().PlayOneShot(Player.SFX_Dash);
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void EndState()
    {
        Player.KnightAnimator.SetBool("Dash", false);
        Player.IsDashing = false;
    }

    public void UnlockDash()
    {
        StateUnlocked = true;
    }
}
