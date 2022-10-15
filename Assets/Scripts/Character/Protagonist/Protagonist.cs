using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class Protagonist : Character
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Weapon currentWeapon;
    private CharacterAttack _attack;
    private Vector3 inputMovement;
    private Coroutine actionRoutine;
    protected override void OnEnable()
    {
        base.OnEnable();
        _inputReader.MovementInputEvent += OnMovementInput;
        _inputReader.AttackEvent += OnAttackInput;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _inputReader.MovementInputEvent -= OnMovementInput;
        _inputReader.AttackEvent -= OnAttackInput;
    }

    private Coroutine TEST;
    protected override void OnActionProcessed(CharacterAction processedAction)
    {
        if (processedAction is AttackAction)
        {
            EndAnimatorLayerLerp();
            const int attackLayer = 1;
            Animator.SetLayerWeight(attackLayer,1);
            Debug.Log(Animator.GetLayerWeight(1));
        }
    }
    protected override void OnActionEnded(CharacterAction endedAction)
    {
        if (inputMovement != Vector3.zero)
            OnMovementInput(inputMovement);
        if (endedAction is AttackAction action)
        {
            StartAnimatorLayerLerp();
        }
    }
    private void OnMovementInput(Vector3 direction)
    {
        inputMovement = direction;
        ActionManager.TryProcessMovement(ref currentAction,inputMovement);
    }
    private void OnAttackInput()
    {
        ActionManager.TryProcessAttack(ref currentAction,currentWeapon);
    }
    private void StartAnimatorLayerLerp()
    {
        const float lerpDuration = 0.8f;
        const float targetWeigth = 0f;
        const string AttackLayer = "AttackLayer";
        actionRoutine = StartCoroutine(Animator.LerpLayerWeigth(AttackLayer, targetWeigth, lerpDuration));
    }
    private void EndAnimatorLayerLerp()
    {
        if (actionRoutine == null) return;
        StopCoroutine(actionRoutine);
        actionRoutine = null;
    }
    










    // UO1 SCRIPT 
    
    
    
    
    
    // private void RecalculateMovement()
    // {
    //     float targetSpeed;
    //     Vector3 adjustedMovement;
    //
    //     if (_gameplayCameraTransform.isSet)
    //     {
    //         //Get the two axes from the camera and flatten them on the XZ plane
    //         Vector3 cameraForward = _gameplayCameraTransform.Value.forward;
    //         cameraForward.y = 0f;
    //         Vector3 cameraRight = _gameplayCameraTransform.Value.right;
    //         cameraRight.y = 0f;
    //
    //         //Use the two axes, modulated by the corresponding inputs, and construct the final vector
    //         adjustedMovement = cameraRight.normalized * _inputVector.x +
    //                            cameraForward.normalized * _inputVector.y;
    //     }
    //     else
    //     {
    //         //No CameraManager exists in the scene, so the input is just used absolute in world-space
    //         Debug.LogWarning("No gameplay camera in the scene. Movement orientation will not be correct.");
    //         adjustedMovement = new Vector3(_inputVector.x, 0f, _inputVector.y);
    //     }
    //
    //     //Fix to avoid getting a Vector3.zero vector, which would result in the player turning to x:0, z:0
    //     if (_inputVector.sqrMagnitude == 0f)
    //         adjustedMovement = transform.forward * (adjustedMovement.magnitude + .01f);
    //
    //     //Accelerate/decelerate
    //     targetSpeed = Mathf.Clamp01(_inputVector.magnitude);
    //     if (targetSpeed > 0f)
    //     {
    //         // This is used to set the speed to the maximum if holding the Shift key,
    //         // to allow keyboard players to "run"
    //         if (isRunning)
    //             targetSpeed = 1f;
    //
    //         if (attackInput)
    //             targetSpeed = .05f;
    //     }
    //     targetSpeed = Mathf.Lerp(_previousSpeed, targetSpeed, Time.deltaTime * 4f);
    //
    //     movementInput = adjustedMovement.normalized * targetSpeed;
    //
    //     _previousSpeed = targetSpeed;
    // }
}
