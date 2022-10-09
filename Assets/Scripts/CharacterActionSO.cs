using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterActionSO : ScriptableObject
{
    [SerializeField] protected ActionKeys actionKey;
    private CharacterAction action;
    public ActionKeys Key => actionKey;
    public CharacterAction GetAction
    {
        get
        {
            if (action != null)
                return this.action;
            action = CreateAction();
            action.ActionSO = this;
            return action;
        }
    }
    protected abstract CharacterAction CreateAction();
}

public abstract class CharacterActionSO<T> : CharacterActionSO where T : CharacterAction, new()
{
    protected override CharacterAction CreateAction() => new T();
}

public enum ActionKeys
{
    None,
    MoveAction,
    AttackAction,
}