using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private List<CharacterActionSO> scriptableActions;
    private Animator animator;
    private NavMeshAgent agent;
    protected CharacterAction[] actions;
    protected CharacterAction currentAction;
    
    public Animator Animator
    {
        get
        {
            if (animator != null) return animator;
            animator = GetComponentInChildren<Animator>();
            return animator;
        }
    }
    public NavMeshAgent Agent
    {
        get
        {
            if (agent != null) return agent;
            agent = GetComponent<NavMeshAgent>();
            return agent;
        }
    }
    protected virtual void Awake()
    {
        SetupActions();
    }

    protected virtual void Update()
    {
        currentAction?.OnUpdate();
    }
    protected void UpdateAnimation()
    {
        
    }
    protected void SetCurrentAction(ActionKeys actionToSet)
    {
        if (currentAction?.Key == actionToSet) return;
        currentAction = GetActionByKey(actionToSet);
    }
    protected void ProcessAction()
    {
        currentAction.ProcessAction();    
    }
    protected void CancelCurrentAction()
    {
        currentAction.CancelAction();
    }
    protected void CancelAllActions()
    {
        foreach (var action in actions)
        {
            action.CancelAction();
        }
    }
    private void SetupActions()
    {
        var count = scriptableActions.Count;
        actions = new CharacterAction[count];
        for (int i = 0; i < count; i++)
        {
            actions[i] = scriptableActions[i].GetAction;
            actions[i].Initialize(this);
        }
    }
    private CharacterAction GetActionByKey(ActionKeys key)
    {
        foreach (var action in actions)
        {
            if (action.Key == key)
                return action;
        }
        return null;
    }
}
