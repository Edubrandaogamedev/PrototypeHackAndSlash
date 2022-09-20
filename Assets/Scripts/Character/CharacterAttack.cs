using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private WeaponSettings _weaponSettings;

    private CharacterAnimation characterAnim;
    private SequenceData currentSequence;

    private int currentComboId;
    private bool isOnCombo;
    private bool inputBuffered;
    
    private Coroutine _checkingComboCO;
    private bool canStartComboAttack => !isOnCombo;
    private void Awake()
    {
        characterAnim = GetComponent<CharacterAnimation>();
    }
    private void UpdateAnimation(bool triggerAttack)
    {
        // if (triggerAttack)
        //     characterAnim.TriggerAttack();
        characterAnim.SetCombo(currentComboId,currentSequence.AnimationMultiplier);
    }
    public void TryAttack()
    {
        if (canStartComboAttack && _checkingComboCO == null)
            StartNextSequence(true);
        else
            inputBuffered = true;
    }
    private void StartNextSequence(bool isFirstSequence = false)
    {
        isOnCombo = true;
        currentComboId++;
        currentSequence = _weaponSettings.ComboSequence[currentComboId - 1];
        UpdateAnimation(isFirstSequence);
        _checkingComboCO = StartCoroutine(CheckingNextSequence());
    }
    private IEnumerator CheckingNextSequence()
    {
        inputBuffered = false;
        var timeUntilNextSequence = currentSequence.TimeToTriggerNextSequence/ currentSequence.AnimationMultiplier;
        yield return new WaitForSeconds(timeUntilNextSequence);
        var comboLength = _weaponSettings.ComboSequence.Length;
        var timeRemaning = characterAnim.GetCurrentAnimationLength() - timeUntilNextSequence;
        if (inputBuffered && currentComboId < comboLength)
            StartNextSequence();
        else
            StartCoroutine(EndingSequence(timeRemaning));
    }

    private IEnumerator EndingSequence(float animTimeRemaining)
    {
        currentComboId = 0;
        UpdateAnimation(false);
        yield return new WaitForSeconds(animTimeRemaining);
        currentSequence = default;
        inputBuffered = false;
        isOnCombo = false;
        _checkingComboCO = null;

    }
    
}
