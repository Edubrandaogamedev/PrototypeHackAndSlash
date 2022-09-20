using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Character/Weapons", fileName = "New Weapon")]
public class WeaponSettings : ScriptableObject
{
    [SerializeField] private SequenceData[] comboSequence;
    public SequenceData[] ComboSequence => comboSequence;
}
[System.Serializable]
public struct SequenceData
{
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;
    [Tooltip("Set the multiplier to speed up the animation")]
    [Min(0)][SerializeField] private float animationMultiplier;
    [Tooltip("Set the time space to move to next combo")]
    [Min(0)][SerializeField] private float timeToTriggerNextSequence;

    public int AttackDamage => attackDamage;

    public float AttackRange => attackRange;

    public float AnimationMultiplier => animationMultiplier;

    public float TimeToTriggerNextSequence => timeToTriggerNextSequence;
}
