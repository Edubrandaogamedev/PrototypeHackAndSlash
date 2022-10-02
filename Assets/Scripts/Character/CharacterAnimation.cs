using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int Speed = Animator.StringToHash("Speed");
    private readonly int ComboID = Animator.StringToHash("ComboID");
    private readonly int ComboSpeed = Animator.StringToHash("ComboSpeed");
    private readonly int Attack = Animator.StringToHash("Attack");
    private readonly int CurrentAnimationLayer = 0;
    private AnimStatesName _statesName;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float movementSpeed)
    {
        _animator.SetFloat(Speed,movementSpeed);    
    }
    public void SetComboAttack(int currentSequence, float animSpeed)
    {
        _animator.SetFloat(ComboSpeed,animSpeed);
        _animator.SetTrigger("Combo"+currentSequence);
        //_animator.SetInteger(ComboID,currentSequence);
    }
    public float GetCurrentAnimationLength()
    {
        return _animator.GetCurrentAnimatorStateInfo(CurrentAnimationLayer).length;
    }

    private bool IsAnimationStatePlaying(string stateName)
    {
        return _animator.GetCurrentAnimatorStateInfo(CurrentAnimationLayer).IsName(stateName);
    }
    private void RestartAnimationState(string stateName)
    {
        _animator.Play(stateName, CurrentAnimationLayer, 0f);
    }
}

enum AnimStatesName
{
    Locomotion,
    BasicAttack,
}
