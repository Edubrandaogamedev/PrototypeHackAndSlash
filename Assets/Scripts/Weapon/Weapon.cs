using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSettings data;
    
    private Collider weaponCollider;
    
    private ComboSequenceData currentComboSequence;
    
    private int currentComboSequenceId;
    
    private bool nextComboTriggered;
    
    public event Action<ComboSequenceData> OnSequenceStarted;
    public event Action<ComboSequenceData> OnSequenceEnded;
    
    public bool IsOnCombo { get; private set; }
    public int CurrentComboSequenceId => currentComboSequenceId;
    private void Awake()
    {
        weaponCollider = GetComponentInChildren<Collider>();
    }
    public void SetWeaponOnCorrectPosition(Vector3 position,Quaternion rotation)
    {
        var weaponTransform = transform;
        weaponTransform.position = position;
        weaponTransform.rotation = rotation;
    }
    public void StartCombo()
    {
        IsOnCombo = true;
        MoveToNextSequence();
    }
    public void TriggerNextSequence()
    {
        nextComboTriggered = true;
    }
    public void EndCombo()
    {
        IsOnCombo = false;
        currentComboSequence = default;
    }
    private void MoveToNextSequence()
    {
        nextComboTriggered = false;
        currentComboSequenceId = CurrentComboSequenceId + 1;
        currentComboSequence = data.ComboSequence[CurrentComboSequenceId - 1];
        OnSequenceStarted(currentComboSequence);
        StartCoroutine(CheckingImpact());
        StartCoroutine(WaitingForNextSequence());
    }
    private IEnumerator WaitingForNextSequence()
    {
        var timeUntilNextSequence = currentComboSequence.TimeToTriggerNextSequence/ currentComboSequence.AnimationMultiplier;
        yield return new WaitForSeconds(timeUntilNextSequence);
        var comboLength = data.ComboSequence.Length;
        if (nextComboTriggered && CurrentComboSequenceId < comboLength)
            MoveToNextSequence();
        else
            EndSequence();    
    }
    private void EndSequence()
    {
        currentComboSequenceId = 0;
        nextComboTriggered = false;
        OnSequenceEnded(currentComboSequence);
    }
    private IEnumerator CheckingImpact()
    {
        yield return new WaitForSeconds(currentComboSequence.StartImpactTime);
        EnableDamageCollider();
        yield return new WaitForSeconds(currentComboSequence.EndImpactTime-currentComboSequence.StartImpactTime);
        DisableDamageCollider();
    }
    private void EnableDamageCollider() => weaponCollider.enabled = true;

    private void DisableDamageCollider() => weaponCollider.enabled = false;
}
