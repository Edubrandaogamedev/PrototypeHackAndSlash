using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Protagonist : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    
    private CharacterNavigation _navigation;
    private CharacterAttack _attack;
    private bool canMove = true;
    private void Awake()
    {
        _navigation = GetComponent<CharacterNavigation>();
        _attack = GetComponent<CharacterAttack>();
        _inputReader.MoveEvent += MovementHandler;
        _inputReader.AttackEvent += AttackHandler;
        _attack.OnAttackStarted += DisableMovement;
        _attack.OnAttackEnded += EnableMovement;
    }
    private void MovementHandler(Vector2 direction)
    {
        _navigation.InputValue = canMove ? new Vector3(direction.x, 0, direction.y) : Vector3.zero;
    }
    private void AttackHandler()
    {
        _attack.TryAttack();
    }
    private void EnableMovement()
    {
        canMove = true;
    }
    private void DisableMovement()
    {
        canMove = false;
        MovementHandler(Vector3.zero);
    }
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
