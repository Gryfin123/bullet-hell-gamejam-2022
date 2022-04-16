using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

[CreateAssetMenu(fileName = "InputReader", menuName = "My SO's/InputReader")]
public class InputScriptableObject : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions
{
    // Gamplay
    public event UnityAction _warpEvent; // = delegate{};
    public event UnityAction _pauseEvent; // = delegate{};
    public event UnityAction<bool> _shootEvent;// = delegate{};
    public event UnityAction<bool> _focusEvent;// = delegate{};
    public event UnityAction<Vector2> _moveEvent;// = delegate{};

    // Menu
    public event UnityAction _clickEvent; // = delegate{};
    public event UnityAction _leaveEvent; // = delegate{};
    

    private GameInput _gameInput;

    private void OnEnable() {
        if (_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
            _gameInput.Menu.SetCallbacks(this);
        }    
        EnableGamplayInput();
    }
    private void OnDisable() {
        DisableAllInput();
    }

    private void EnableGamplayInput()
    {
        _gameInput.Gameplay.Enable();
        _gameInput.Menu.Disable();
    }
    private void EnableMenuInput()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Menu.Enable();
    }
    private void DisableAllInput()
    {
        _gameInput.Gameplay.Disable();
        _gameInput.Menu.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFocus(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _focusEvent.Invoke(true);
        if (_warpEvent != null
            && context.phase == InputActionPhase.Canceled)
            _focusEvent.Invoke(false);
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _shootEvent.Invoke(true);
        if (_warpEvent != null
            && context.phase == InputActionPhase.Canceled)
            _shootEvent.Invoke(false);
    }

    public void OnWarp(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _warpEvent.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _pauseEvent.Invoke();
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _clickEvent.Invoke();
    }
    public void OnLeave(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _leaveEvent.Invoke();
    }
}
