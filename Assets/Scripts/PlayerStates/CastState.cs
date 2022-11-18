using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastState : State
{
    public CastState(Knight player) { Player = player; }
    public override void BeginState()
    {
        Player.KnightAnimator.SetBool("Cast", true);
        Player.CastSpell();
        StateUnlocked = false;
        Player.ProjectileCooldownUI.GetComponent<Image>().color = new Color(255, 0, 0);
    }

    // Update is called once per frame
    public override void Update()
    {
       
    }

    public override void EndState()
    {
        Player.KnightAnimator.SetBool("Cast", false);
        Player.StartCastTimer = true;
    }

}
