using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackComboWatcher : MonoBehaviour
{
    public event Action OnAttackEnded = delegate {};

    private void AttackEnded()
    {
        OnAttackEnded();
    }
}
