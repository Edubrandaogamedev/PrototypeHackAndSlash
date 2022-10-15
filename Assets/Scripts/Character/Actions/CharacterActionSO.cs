using UnityEngine;

public abstract class CharacterActionSO : ScriptableObject
{
#if UNITY_EDITOR
    [Header("Editor Use Only")]
    public bool settingNeeded;
#endif
    [Space] [Header("Gameplay")]
    [SerializeField] protected ActionKeys actionKey;
    [Tooltip("If this action can be interrupt for another one set this to true. Example: The movement action can be instantly changed to another action")]
    [SerializeField] protected bool canBeOverridden;
    
    private CharacterAction action;
    public ActionKeys Key => actionKey;
    public bool Overridden => canBeOverridden;
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
    None = 0,
    MoveAction,
    AttackAction,
}