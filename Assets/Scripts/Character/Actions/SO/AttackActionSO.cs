using System.Collections;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Action", menuName = "Game/Character/Actions/AttackAction")]
public class AttackActionSO : CharacterActionSO<AttackAction>
{
    [Header("Animator Parameters")][Space]
    public ComboSequenceIdAnimatorParameterSO[] animatorComboIdParameterSOs;
    public ComboSequenceSpeedAnimatorParameterSO animatorComboSpeedParameterSO;
    private void OnEnable()
    {
        actionKey = ActionKeys.AttackAction;
        animatorComboIdParameterSOs = animatorComboIdParameterSOs.OrderBy(order => order.id).ToArray();
    }
    public ComboSequenceIdAnimatorParameterSO GetComboIDParameterByID(int comboID)
    {
        return comboID <= 0 ? animatorComboIdParameterSOs[0] : animatorComboIdParameterSOs[comboID - 1];
    }
}
public class AttackAction : CharacterAction
{
    public Weapon Weapon { get; private set;}
    private AttackActionSO actionSO => (AttackActionSO)base.ActionSO;
    private ComboSequenceIdAnimatorParameter comboIDAnimator => (ComboSequenceIdAnimatorParameter) actionSO.GetComboIDParameterByID(Weapon.CurrentComboSequenceId).GetAnimParameter;
    private ComboSequenceSpeedAnimatorParameter comboSpeedAnimator => (ComboSequenceSpeedAnimatorParameter) actionSO.animatorComboSpeedParameterSO.GetAnimParameter;
    public void WeaponSetup(Weapon usingWeapon)
    {
        if (usingWeapon == Weapon) return;
        DisposeWeaponEvents();
        Weapon = usingWeapon;
        Weapon.OnSequenceStarted += ComboSequenceChangeHandler;
        Weapon.OnSequenceEnded += EndingComboHandler;
    }
    public void TriggerNextCombo()
    {
        if (Weapon == null) return;
        Weapon.TriggerNextSequence();
    }
    private void DisposeWeaponEvents()
    {
        if (Weapon == null) return;
        Weapon.OnSequenceStarted -= ComboSequenceChangeHandler;
        Weapon.OnSequenceEnded -= EndingComboHandler;
    }
    private void ComboSequenceChangeHandler(ComboSequenceData currentSquence)
    {
        comboIDAnimator.UpdateAnimator();
        comboSpeedAnimator.UpdateAnimator(currentSquence.AnimationMultiplier);
    }
    
    private void EndingComboHandler(ComboSequenceData currentSquence)
    {
        Weapon.EndCombo();
        CancelAction();
    }
    
    private void InitializeAnimations()
    {
        foreach (var comboIDAnim in actionSO.animatorComboIdParameterSOs)
        {
            comboIDAnim.GetAnimParameter.Initialize(character.Animator);
        }
        comboSpeedAnimator.Initialize(character.Animator);
    }
    public override void Initialize(Character originCharacter, ActionSetting setting = null)
    {
        character = originCharacter;
        InitializeAnimations();
    }
    public override void ProcessAction()
    {
        BeingProcessed = true;
        OnActionProcessed();
        Weapon.StartCombo();
    }
    public override void CancelAction()
    {
        Debug.Log("ATTACK ACTION DONE");
        BeingProcessed = false;
        OnActionEnded();
    }
    public override void OnUpdate()
    {
        
    }
}
