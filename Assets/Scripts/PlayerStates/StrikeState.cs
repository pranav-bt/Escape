using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeState : State
{
    // Start is called before the first frame update
    public StrikeState(Knight player) { Player = player; }
    public StrikeState(Knight player, bool Unlocked) { Player = player; StateUnlocked = Unlocked; }
    public override void BeginState()
    {
        Player.KnightAnimator.SetBool("Strike", true);
        GameObject.FindObjectOfType<AudioPlayer>().PlayOneShot(Player.SFX_Strike);
        Player.IsStriking = true;
    }

    // Update is called once per frame
    public override void Update()
    {

    }

    public override void EndState()
    {
        Player.KnightAnimator.SetBool("Strike", false);
        Player.IsStriking = false;
    }

    public void UnlockStrike()
    {
        StateUnlocked = true;
    }
}
