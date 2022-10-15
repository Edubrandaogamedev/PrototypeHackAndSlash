using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AnimatorComboSpeedParameter", menuName = "Game/Animator/Character/ComboSpeed")]
public class ComboSequenceSpeedAnimatorParameterSO : ActionAnimatorParameterSO<ComboSequenceSpeedAnimatorParameter>
{
    private void OnEnable()
    {
        parameterType = ParameterType.Float;
    }
}
public class ComboSequenceSpeedAnimatorParameter : ActionAnimatorParameter
{
    public void UpdateAnimator(float speedValue)
    {
        base.floatValue = speedValue;
        base.UpdateAnimator();
    }
}
