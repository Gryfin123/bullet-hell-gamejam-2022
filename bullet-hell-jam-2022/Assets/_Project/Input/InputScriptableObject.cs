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
    [SerializeField] private Vector2 _movementAxis;
    [SerializeField] private bool _isFocusing;
    [SerializeField] private bool _isShooting;

    // Public
    public bool IsFocusing()
    {
        return _isFocusing;
    }
    public bool IsShootinging()
    {
        return _isShooting;
    }
    public Vector2 GetMovementAxis()
    {
        return _movementAxis;
    }

    // Private
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
        ResetVariables();
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
    private void ResetVariables()
    {
        _isFocusing = false;
        _isShooting = false;
        _movementAxis = new Vector2(0,0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            _movementAxis = context.ReadValue<Vector2>();
        if (context.phase == InputActionPhase.Performed)
            _movementAxis = context.ReadValue<Vector2>();

        if (_moveEvent != null)
        {
            _moveEvent.Invoke(_movementAxis);
        }
    }

    public void OnFocus(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            _isFocusing = true;
        if (context.phase == InputActionPhase.Canceled)
            _isFocusing = false;
        
        if (_focusEvent != null)
        _focusEvent.Invoke(_isFocusing);
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            _isShooting = true;
        if (context.phase == InputActionPhase.Canceled)
            _isShooting = false;
            
        if (_shootEvent != null)
        _shootEvent.Invoke(_isShooting);
    }

    public void OnWarp(InputAction.CallbackContext context)
    {
        if (_warpEvent != null
            && context.phase == InputActionPhase.Performed)
            _warpEvent.Invoke();
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (_pauseEvent != null
            && context.phase == InputActionPhase.Performed)
            _pauseEvent.Invoke();
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (_clickEvent != null
            && context.phase == InputActionPhase.Performed)
            _clickEvent.Invoke();
    }
    public void OnLeave(InputAction.CallbackContext context)
    {
        if (_leaveEvent != null
            && context.phase == InputActionPhase.Performed)
            _leaveEvent.Invoke();
    }
}
