using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    private GameInput _gameInput;
    public event Action<Vector2> MoveEvent = delegate { };
    public event System.Action AttackEvent = delegate { };
    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            //_gameInput.Dialogues.SetCallbacks(this);
        }
        EnableGameplayInput();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent(context.ReadValue<Vector2>());
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            AttackEvent();
    }

    public void EnableDialogueInput()
    {
        //_gameInput.Dialogues.Enable();
        _gameInput.Gameplay.Disable();
    }

    public void EnableGameplayInput()
    {
        _gameInput.Gameplay.Enable();
        //_gameInput.Dialogues.Disable();
    }

    public void DisableAllInput()
    {
        _gameInput.Gameplay.Disable();
        //_gameInput.Dialogues.Disable();
    }
}
