using System.Collections;
using UnityEngine;

public class ActionAnimatorParameter
{
    internal ActionAnimatorParameterSO animatorParameterSO;
    private Animator _animator;
    private int _parameterHash;
    
    protected bool boolValue;
    protected float floatValue;
    protected int intValue;

    public void Initialize(Animator characterAnimator)
    {
        _animator = characterAnimator;
        _parameterHash = Animator.StringToHash(animatorParameterSO.ParameterName);
    }
    public virtual void UpdateAnimator()
    {
        SetParameter();
    }
    private void SetParameter()
    {
        switch (animatorParameterSO.ParamType)
        {
            case ActionAnimatorParameterSO.ParameterType.Bool:
                _animator.SetBool(_parameterHash, boolValue);
                break;
            case ActionAnimatorParameterSO.ParameterType.Int:
                _animator.SetInteger(_parameterHash, intValue);
                break;
            case ActionAnimatorParameterSO.ParameterType.Float:
                _animator.SetFloat(_parameterHash, floatValue);
                break;
            case ActionAnimatorParameterSO.ParameterType.Trigger:
                _animator.SetTrigger(_parameterHash);
                break;
            default:
                break;
        }
    }
}
