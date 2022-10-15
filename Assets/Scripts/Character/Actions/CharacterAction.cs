using System;

public abstract class CharacterAction : IAction
{
    protected Character character;
    internal CharacterActionSO ActionSO;
    public ActionKeys Key => ActionSO.Key;
    public bool IsDone => ActionSO.Overridden || !BeingProcessed;
    protected bool BeingProcessed {get; set;}
    public event Action<CharacterAction> OnActionProcessedEvent = delegate{};
    public event Action<CharacterAction> OnActionEndedEvent = delegate {};
    public abstract void Initialize(Character originCharacter, ActionSetting setting = null);
    public abstract void ProcessAction();
    public abstract void CancelAction();
    public abstract void OnUpdate();
    protected virtual void OnActionProcessed()
    {
        OnActionProcessedEvent(this);
    }
    protected virtual void OnActionEnded()
    {
        OnActionEndedEvent(this);
    }
}
