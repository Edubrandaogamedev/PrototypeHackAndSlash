using System.Linq;
using UnityEngine;

public class CharacterActionManager
{
    private readonly CharacterAction[] actions;
    public CharacterActionManager(CharacterAction[] characterActions)
    {
        actions = new CharacterAction[characterActions.Length];
        actions = characterActions;
    }
    public void TryProcessMovement(ref CharacterAction currentAction,Vector3 inputValue)
    {
        if (currentAction is {IsDone: false}) return;
        var actionToProcess = (MovementAction)GetActionByKey(ActionKeys.MoveAction);
        actionToProcess.ModifyInputValue(inputValue);
        SetNewActiveAction(ref currentAction,actionToProcess);
        currentAction?.ProcessAction();    
    }
    public void TryProcessAttack(ref CharacterAction currentAction,Weapon characterWeapon)
    {
        var actionToProcess = (AttackAction)GetActionByKey(ActionKeys.AttackAction);
        switch (currentAction)
        {
            case AttackAction:
                if (currentAction.IsDone)
                    currentAction?.ProcessAction();
                else
                    actionToProcess.TriggerNextCombo();
                return;
            case {IsDone:false}:
                return;
        }
        currentAction?.CancelAction();
        actionToProcess.WeaponSetup(characterWeapon);
        SetNewActiveAction(ref currentAction,actionToProcess);
        currentAction?.ProcessAction();    
    }
    private void SetNewActiveAction(ref CharacterAction currentAction,CharacterAction actionToProcess)
    {
        if (currentAction?.Key == actionToProcess.Key) return;
        currentAction = actionToProcess;
    }
    private CharacterAction GetActionByKey(ActionKeys key)
    {
        return actions.FirstOrDefault(action => action.Key == key);
    }
}