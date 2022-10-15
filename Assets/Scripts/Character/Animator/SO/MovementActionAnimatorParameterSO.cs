using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorMovementParameter", menuName = "Game/Animator/Character/MovementParameter")]
public class MovementActionAnimatorParameterSO : ActionAnimatorParameterSO<MovementActionAnimatorParameter>
{
    private void OnEnable()
    {
        parameterType = ParameterType.Float;
    }
}

public class MovementActionAnimatorParameter : ActionAnimatorParameter
{
    public void UpdateAnimator(float movementValue)
    {
        base.floatValue = movementValue;
        base.UpdateAnimator();
    }
}