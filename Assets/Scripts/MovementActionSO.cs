using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Movement Action", menuName = "Game/Character/Actions/MovementAction")]
public class MovementActionSO : CharacterActionSO<MovementAction>
{ 
    public MovementActionAnimatorParameterSO animatorMovementParameterSO;
    public CharacterMovementSettings movementSettings;
    public NavAgentSettingsOverride agentSettings;
}
public class MovementAction : CharacterAction
{
    private MovementActionSO actionSO => (MovementActionSO)base.ActionSO;
    private MovementActionAnimatorParameter animatorActionParameter => (MovementActionAnimatorParameter) actionSO.animatorMovementParameterSO.GetAnimParameter;
    private Character character;
    private Vector3 inputValue;
    public override void Initialize(Character originCharacter)
    {
        character = originCharacter;
        InitializeAnimations();
        SettingsSetup();
    }
    private void SettingsSetup()
    {
        character.Agent.speed = actionSO.agentSettings.Speed;
        character.Agent.angularSpeed = actionSO.agentSettings.AngularSpeed;
        character.Agent.acceleration = actionSO.agentSettings.Acceleration;
    }
    private void InitializeAnimations()
    {
        animatorActionParameter.Initialize(character.Animator);
    }
    public override void ProcessAction()
    {
            
    }
    public override void CancelAction()
    {
    }

    public override void OnUpdate()
    {
        if (inputValue.magnitude <= actionSO.movementSettings.ControllerInputThreshold)
        {
            animatorActionParameter.UpdateAnimator(0);
        }
        else
        {
            if (actionSO.movementSettings.UsePhysics)
            {
                character.Agent.SetDestination(GetDestination());
            }
            else
            {
                character.transform.position = GetDestination();
                RotateTowardsDirection();
            }
            animatorActionParameter.UpdateAnimator(inputValue.magnitude);
        }
    }
    public void ModifyInputValue(Vector3 newInputValue)
    {
        inputValue = newInputValue;
    }
    private Vector3 GetDestination()
    {
        var destination = character.transform.position + inputValue * Time.deltaTime * actionSO.movementSettings.Speed;
        return NavMesh.SamplePosition(destination, out var hit, .3f, NavMesh.AllAreas) ? hit.position : character.transform.position;
    }
    private void RotateTowardsDirection()
    {
        var angle = 90 - Mathf.Atan2(inputValue.z, inputValue.x) * Mathf.Rad2Deg;
        var euler = Quaternion.Euler(character.transform.localEulerAngles);
        var newRot = Quaternion.Euler(euler.x, angle, euler.z);
        character.transform.localRotation = newRot;
    }
    
}
