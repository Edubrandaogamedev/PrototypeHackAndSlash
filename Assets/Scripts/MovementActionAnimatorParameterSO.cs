using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimatorMovementParameter", menuName = "Game/Animator/Character/MovementParameter")]
public class MovementActionAnimatorParameterSO : ActionAnimatorParameterSO<MovementActionAnimatorParameter>
{
}

public class MovementActionAnimatorParameter : ActionAnimatorParameter
{
    public void UpdateAnimator(float movementValue)
    {
        base.floatValue = movementValue;
        base.UpdateAnimator();
    }
}