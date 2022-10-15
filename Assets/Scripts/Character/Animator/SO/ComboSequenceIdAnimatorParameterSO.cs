using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AnimatorComboIDParameter", menuName = "Game/Animator/Character/ComboId")]
public class ComboSequenceIdAnimatorParameterSO : ActionAnimatorParameterSO<ComboSequenceIdAnimatorParameter>
{
    [Min(1)] public int id = 0;
    private void OnEnable()
    {
        parameterType = ParameterType.Trigger;
    }
}
public class ComboSequenceIdAnimatorParameter : ActionAnimatorParameter
{
}
