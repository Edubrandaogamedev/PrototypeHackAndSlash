using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAnimatorParameterSO : ScriptableObject
{
    [SerializeField] private ParameterType parameterType = default;
    [SerializeField] private string parameterName = default;
    private ActionAnimatorParameter animatorParameter;
    public enum ParameterType
    {
        Bool, Int, Float, Trigger
    }
    public ParameterType ParamType => parameterType;
    public string ParameterName => parameterName;

    public ActionAnimatorParameter GetAnimParameter
    {
        get
        {
            if (animatorParameter != null)
                return this.animatorParameter;
            animatorParameter = CreateAction();
            animatorParameter.animatorParameterSO = this;
            return animatorParameter;
        }
    }
    protected abstract ActionAnimatorParameter CreateAction();
}

public abstract class ActionAnimatorParameterSO<T> : ActionAnimatorParameterSO where T : ActionAnimatorParameter, new()
{
    protected override ActionAnimatorParameter CreateAction() => new T();
}
