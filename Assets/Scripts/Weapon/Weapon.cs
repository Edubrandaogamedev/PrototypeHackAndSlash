using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSettings data;

    private float damage;
    
    private WeaponImpact weaponCollider;
    
    private ComboSequenceData currentComboSequence;
    
    private int currentComboSequenceId;
    
    private bool nextComboTriggered;
    
    public event Action<ComboSequenceData> OnSequenceStarted;
    public event Action<ComboSequenceData> OnSequenceEnded;
    public bool IsOnCombo { get; private set; }
    public int CurrentComboSequenceId => currentComboSequenceId;
    public ComboSequenceData CurrentComboSequence => currentComboSequence;
    private void Awake()
    {
        weaponCollider = GetComponentInChildren<WeaponImpact>();
        weaponCollider.OnHit += OnWeaponHit;
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
        Debug.Log("<color=blue> END COMBO </color>");
        IsOnCombo = false;
        currentComboSequence = default;
    }
    private void MoveToNextSequence()
    {
        nextComboTriggered = false;
        currentComboSequenceId = currentComboSequenceId >= data.ComboSequence.Length? 1: CurrentComboSequenceId + 1;
        currentComboSequence = data.ComboSequence[CurrentComboSequenceId - 1];
        Debug.Log("<color=red> THE NEXT SEQUENCE I MOVED WAS: </color>" + CurrentComboSequenceId);
        //currentComboSequenceId = CurrentComboSequenceId + 1;
        OnSequenceStarted(currentComboSequence);
        StartCoroutine(CheckingImpact());
        StartCoroutine(WaitingForNextSequence());
    }
    private IEnumerator WaitingForNextSequence()
    {
        var timeUntilNextSequence = currentComboSequence.TimeToTriggerNextSequence/ currentComboSequence.AnimationMultiplier;
        yield return new WaitForSeconds(timeUntilNextSequence);
        Debug.Log("<color=orange> CURRENT SEQUENCE IS </color>:" + currentComboSequenceId);
        var comboLength = data.ComboSequence.Length;
        Debug.Log("<color=green> GOING TO NEXT COMBO? </color>" + (nextComboTriggered && CurrentComboSequenceId <= comboLength));
        if (nextComboTriggered && CurrentComboSequenceId <= comboLength)
            MoveToNextSequence();
        else
            EndSequence();    
    }
    private void EndSequence()
    {
        Debug.Log("END SEQUENCE");
        currentComboSequenceId = 0;
        nextComboTriggered = false;
        OnSequenceEnded(currentComboSequence);
    }
    private IEnumerator CheckingImpact()
    {
        yield return new WaitForSeconds(currentComboSequence.StartImpactTime);
        weaponCollider.EnableCollider();
        yield return new WaitForSeconds(currentComboSequence.EndImpactTime-currentComboSequence.StartImpactTime);
        weaponCollider.DisableCollider();
    }
    private void OnWeaponHit(ITakeDamage damageableEntity)
    {
        damageableEntity.TakeDamage(this);
    }
}
