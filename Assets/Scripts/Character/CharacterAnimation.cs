using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int ComboID = Animator.StringToHash("ComboID");
    private static readonly int ComboSpeed = Animator.StringToHash("ComboSpeed");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float movementSpeed)
    {
        _animator.SetFloat(Speed,movementSpeed);    
    }
    public void TriggerAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void SetCombo(int currentSequence, float animSpeed)
    {
        _animator.SetFloat(ComboSpeed,animSpeed);
        _animator.SetInteger(ComboID,currentSequence);
    }
    
    public float GetCurrentAnimationLength()
    {
        return _animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
