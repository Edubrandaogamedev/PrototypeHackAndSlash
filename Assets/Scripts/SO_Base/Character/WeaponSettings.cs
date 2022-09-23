using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Character/Weapons", fileName = "New Weapon")]
public class WeaponSettings : ScriptableObject
{
    [SerializeField] private ComboSequenceData[] comboSequence;
    public ComboSequenceData[] ComboSequence => comboSequence;
}
[System.Serializable]
public struct ComboSequenceData
{
    [Header("Attack Settings")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [Header("Animation Settings")]
    [SerializeField] private float startImpactTime;
    [SerializeField] private float endImpactTime;
    
    [Tooltip("Set the multiplier to speed up the animation")]
    [Min(1)][SerializeField] private float animationMultiplier;
    [Tooltip("Set the time space to move to next combo")]
    [Min(0)][SerializeField] private float timeToTriggerNextSequence;
    
    [Header("Not implemented")]
    [Tooltip("Define how many hits this sequence can do at the same enemy")]
    [SerializeField] private int multipliesAttacks;
    [Tooltip("Set to true if you want to hit multiple enemies at the same hit")]
    [SerializeField] private bool isAOE;
    public float AttackDamage => attackDamage;
    public float AttackRange => attackRange;
    public float AnimationMultiplier => animationMultiplier;
    public float TimeToTriggerNextSequence => timeToTriggerNextSequence;
    public float StartImpactTime => startImpactTime;
    public float EndImpactTime => endImpactTime;
}
