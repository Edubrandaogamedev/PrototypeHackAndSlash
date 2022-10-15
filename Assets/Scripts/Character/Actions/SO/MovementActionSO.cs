using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Movement Action", menuName = "Game/Character/Actions/MovementAction")]
public class MovementActionSO : CharacterActionSO<MovementAction>
{
    public MovementActionAnimatorParameterSO animatorMovementParameterSO;
    private void OnEnable()
    {
        actionKey = ActionKeys.MoveAction;
    }
}
public class MovementAction : CharacterAction
{
    private MovementActionSO actionSO => (MovementActionSO)base.ActionSO;
    private MovementActionAnimatorParameter animatorActionParameter => (MovementActionAnimatorParameter) actionSO.animatorMovementParameterSO.GetAnimParameter;
    private CharacterMovementSettings actionSetting;
    private Vector3 inputValue;
    public override void Initialize(Character originCharacter, ActionSetting setting = null)
    {
        character = originCharacter;
        actionSetting = (CharacterMovementSettings) setting;
        InitializeAnimations();
    }
    private void InitializeAnimations()
    {
        animatorActionParameter.Initialize(character.Animator);
    }
    public override void ProcessAction()
    {
        OnActionProcessed();
    }
    public override void CancelAction()
    {
        Debug.Log("CANCELING THE MOVEMENT ACTION");
        inputValue = Vector3.zero;
        animatorActionParameter.UpdateAnimator(0);
        OnActionEnded();
    }
    public override void OnUpdate()
    {
        if (inputValue.magnitude <= actionSetting.ControllerInputThreshold)
        {
            animatorActionParameter.UpdateAnimator(0);
        }
        else
        {
            if (actionSetting.UsePhysics)
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
        var destination = character.transform.position + inputValue * Time.deltaTime * actionSetting.Speed;
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
