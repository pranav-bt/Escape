using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void InGameEvent();
    public static event InGameEvent DamageEvent;
    public static event InGameEvent GameOver;

    public static void BroadCastDamageEvent()
    {
        if(DamageEvent!=null)
        {
            DamageEvent();
        }
    }
}
