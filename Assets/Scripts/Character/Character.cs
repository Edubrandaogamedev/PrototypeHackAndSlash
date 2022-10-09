using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings settings;
    private Animator animator;
    private NavMeshAgent agent;
    private CharacterAction[] actions;
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
    private void SetupActions()
    {
        var count = settings.ScriptableActions.Length;
        actions = new CharacterAction[count];
        for (int i = 0; i < count; i++)
        {
            var scriptableAction = settings.ScriptableActions[i];
            actions[i] = scriptableAction.GetAction;
            actions[i].Initialize(this,settings.GetSettingForAction(scriptableAction));
        }
    }
    private void SetupComponentsOverride()
    {
        Agent.speed = settings.AgentOverride.Speed;
        Agent.angularSpeed = settings.AgentOverride.AngularSpeed;
        Agent.acceleration = settings.AgentOverride.Acceleration;
    }
    protected virtual void Awake()
    {
        SetupActions();
        SetupComponentsOverride();
    }
    protected virtual void Update()
    {
        currentAction?.OnUpdate();
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
