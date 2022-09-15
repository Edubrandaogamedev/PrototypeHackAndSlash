using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetSpeed(float movementSpeed)
    {
        _animator.SetFloat(Speed,movementSpeed);    
    }
}
