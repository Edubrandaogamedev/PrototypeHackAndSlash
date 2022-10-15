// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //TODO TO DELETE THIS SCRIPT
// public class CharacterAnimation : MonoBehaviour
// {
//     private Animator _animator;
//     private ITakeDamage _damageable;
//     private readonly int Speed = Animator.StringToHash("Speed");
//     private readonly int ComboID = Animator.StringToHash("ComboID");
//     private readonly int ComboSpeed = Animator.StringToHash("ComboSpeed");
//     private readonly int Attack = Animator.StringToHash("Attack");
//     private readonly int Hit = Animator.StringToHash("Hit");
//     private readonly int CurrentAnimationLayer = 0;
//     private AnimStatesName _statesName;
//
//     private void Awake()
//     {
//         _animator = GetComponentInChildren<Animator>();
//         _damageable = GetComponent<ITakeDamage>();
//         
//     }
//     private void OnEnable()
//     {
//         if (_damageable != null)
//             _damageable.OnReceiveHit += OnCharacterTakeHit;
//     }
//     private void OnDisable()
//     {
//         if (_damageable != null)
//             _damageable.OnReceiveHit -= OnCharacterTakeHit;
//     }
//     public void SetSpeed(float movementSpeed)
//     {
//         _animator.SetFloat(Speed,movementSpeed);    
//     }
//     public void SetComboAttack(int currentSequence, float animSpeed)
//     {
//         _animator.SetFloat(ComboSpeed,animSpeed);
//         _animator.SetTrigger("Combo"+currentSequence);
//     }
//
//     private void OnCharacterTakeHit()
//     {
//         _animator.SetTrigger(Hit);
//     }
//     public float GetCurrentAnimationLength()
//     {
//         return _animator.GetCurrentAnimatorStateInfo(CurrentAnimationLayer).length;
//     }
// }
//
// enum AnimStatesName
// {
//     Locomotion,
//     BasicAttack,
// }
