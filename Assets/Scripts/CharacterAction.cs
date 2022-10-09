using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAction : IAction
{
    internal CharacterActionSO ActionSO;
    public ActionKeys Key => ActionSO.Key;
    public abstract void Initialize(Character originCharacter);
    public abstract void ProcessAction();
    public abstract void CancelAction();
    public abstract void OnUpdate();
}
