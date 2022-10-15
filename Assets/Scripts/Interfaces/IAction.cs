using System;

public interface IAction
{
    public void ProcessAction();
    public void CancelAction();
    public void OnUpdate();
    public event Action<CharacterAction> OnActionProcessedEvent;
    public event Action<CharacterAction> OnActionEndedEvent;
}
