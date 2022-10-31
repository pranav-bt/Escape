using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected Knight Player;
    protected Zombie Owner;
    public bool StateUnlocked = true;
    public State() { }
    public virtual void BeginState() { }
    public virtual void Update() { }
    public virtual void EndState() { }
}
