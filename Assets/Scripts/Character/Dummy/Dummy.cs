using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, ITakeDamage
{
    public event System.Action OnReceiveHit = delegate {};
    public void TakeDamage(Weapon hitBy)
    {
        OnReceiveHit();    
    }
}
