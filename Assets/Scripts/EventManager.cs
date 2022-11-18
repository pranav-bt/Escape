using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void InGameEvent();
    public static event InGameEvent DamageEvent;
    public static event InGameEvent GameOver;
    public static event InGameEvent CoinCollected;
    public static event InGameEvent CharacterSwitch;
    public static event InGameEvent PlayerWin;
    public static event InGameEvent Teleport;
    public static event InGameEvent MapView;


    public static void BroadCastDamageEvent()
    {
        if(DamageEvent!=null)
        {
            DamageEvent();
        }
    }

    public static void BroadCastGameOverEvent()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }

    public static void BroadCastCoinCollectedEvent()
    {
        if (CoinCollected != null)
        {
            CoinCollected();
        }
    }

    public static void BroadCastCharacterSwitchEvent()
    {
        if (CharacterSwitch != null)
        {
            CharacterSwitch();
        }
    }

    public static void BroadCastPlayerWinEvent()
    {
        if (PlayerWin != null)
        {
            PlayerWin();
        }
    }

    public static void BroadCastMapViewEvent()
    {
        if(MapView != null)
        {
            MapView();
        }
    }
}
