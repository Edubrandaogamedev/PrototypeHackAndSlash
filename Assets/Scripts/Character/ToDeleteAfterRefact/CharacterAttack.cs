using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    
    //private CharacterAnimation characterAnim;
    private bool canStartComboAttack => !currentWeapon.IsOnCombo;
    public event Action OnAttackStarted = delegate {};
    public event Action OnAttackEnded = delegate {};
    private void Awake()
    {
        //characterAnim = GetComponent<CharacterAnimation>();
        SetupCurrentWeapon();
    }
    private void UpdateAnimation(ComboSequenceData currentSquence)
    {
        //characterAnim.SetComboAttack(currentWeapon.CurrentComboSequenceId,currentSquence.AnimationMultiplier);
    }
    private void SetupCurrentWeapon()
    {
        currentWeapon.OnSequenceStarted += ComboSequenceChangeHandler;
        currentWeapon.OnSequenceEnded += EndingComboHandler;
    }
    // private void RemoveCurrentWeapon()
    // {
    //     currentWeapon.OnSequenceStarted -= SequenceChangeHandler;
    //     currentWeapon.OnSequenceEnded -= SequenceChangeHandler;
    // }
    public void TryAttack()
    {
        if (canStartComboAttack)
            DoAttack();
        else
            currentWeapon.TriggerNextSequence();
    }
    private void DoAttack()
    {
        OnAttackStarted();
        currentWeapon.StartCombo();
    }
    private void EndAttack()
    {
        currentWeapon.EndCombo();
        OnAttackEnded();
    }
    private void ComboSequenceChangeHandler(ComboSequenceData currentSquence)
    {
        UpdateAnimation(currentSquence);
    }
    private void EndingComboHandler(ComboSequenceData currentSquence)
    {
        //UpdateAnimation(currentSquence);
        currentWeapon.EndCombo();
        OnAttackEnded();
        //StartCoroutine(WaitForSequenceAnimationEnd(currentSquence));
    }
    private IEnumerator WaitForSequenceAnimationEnd(ComboSequenceData sequence)
    {
        var elapsedTime = sequence.TimeToTriggerNextSequence/ sequence.AnimationMultiplier;
        //var timeRemaning = characterAnim.GetCurrentAnimationLength() - elapsedTime;
        //yield return new WaitForSeconds(timeRemaning);
        yield return null;
        EndAttack();
    }
}
