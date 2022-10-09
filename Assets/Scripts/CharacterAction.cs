using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : IAction
{
    protected Character character;
    internal CharacterActionSO ActionSO;
    public ActionKeys Key => ActionSO.Key;
    public abstract void Initialize(Character originCharacter, ActionSetting setting = null);
    public abstract void ProcessAction();
    public abstract void CancelAction();
    public abstract void OnUpdate();
}
