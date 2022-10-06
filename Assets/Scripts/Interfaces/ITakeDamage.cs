using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(Weapon hitBy);
    event Action OnReceiveHit;
    
}
