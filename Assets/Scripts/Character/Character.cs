using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterSettings settings;
    private Animator animator;
    private NavMeshAgent agent;
    private CharacterAction[] actions;
    private CharacterActionManager _characterActionCharacterActionManager;
    protected CharacterAction currentAction;
    protected CharacterActionManager ActionManager
    {
        get
        {
            if (_characterActionCharacterActionManager != null) return _characterActionCharacterActionManager;
            _characterActionCharacterActionManager = new CharacterActionManager(actions);
            return _characterActionCharacterActionManager;
        }
    }
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
    protected  abstract void OnActionProcessed(CharacterAction processedAction);
    protected abstract void OnActionEnded(CharacterAction endedAction);
    protected virtual void OnEnable()
    {
       InitializeCharacter();
    }
    protected virtual void OnDisable()
    {
        DisposeEvents();
    }
    protected virtual void Update()
    {
        currentAction?.OnUpdate();
    }
    private void InitializeCharacter()
    {
        SetupActions();
        SetupComponentsOverride();
        _characterActionCharacterActionManager = new CharacterActionManager(actions);
    }
    private void SetupActions()
    {
        var count = settings.ScriptableActions.Length;
        actions = new CharacterAction[count];
        for (var i = 0; i < count; i++)
        {
            var scriptableAction = settings.ScriptableActions[i];
            actions[i] = scriptableAction.GetAction;
            actions[i].Initialize(this,settings.GetSettingForAction(scriptableAction));
            actions[i].OnActionProcessedEvent += OnActionProcessed;
            actions[i].OnActionEndedEvent += OnActionEnded;
        }
    }
    private void DisposeEvents()
    {
        foreach (var action in actions)
        {
            action.OnActionProcessedEvent -= OnActionProcessed;
            action.OnActionEndedEvent -= OnActionEnded;
        }
    }
    private void SetupComponentsOverride()
    {
        Agent.speed = settings.AgentOverride.Speed;
        Agent.angularSpeed = settings.AgentOverride.AngularSpeed;
        Agent.acceleration = settings.AgentOverride.Acceleration;
    }
}

public static class AnimatorLayerController
{
    public static IEnumerator LerpLayerWeigth(this Animator original, string layerName,float targetWeigth, float duration, float lerpSpeed = 1)
    {
        var layerIndex = original.GetLayerIndex(layerName);
        var startWeight = original.GetLayerWeight(layerIndex);
        var currentWeight = startWeight;
        float timeElapsed = 0;
        while (currentWeight > 0)
        {
            currentWeight = Mathf.Lerp(1, 0, timeElapsed / duration);
            timeElapsed += Time.deltaTime*lerpSpeed;
            original.SetLayerWeight(layerIndex, currentWeight);
            yield return null;
        }
        original.SetLayerWeight(layerIndex, targetWeigth);
    }
}