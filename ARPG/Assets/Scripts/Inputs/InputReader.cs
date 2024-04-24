using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
{
    private GameInput _gameInput;
    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput=new GameInput();
            _gameInput.Player.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
            SetGameplay();
        }
    }
    private void OnDisable()
    {
        
        _gameInput.UI.Enable();
        _gameInput.Player.Enable();
        _gameInput.Player.LeftMouseB.Disable();

    }

    public void SetGameplay()
    {
        _gameInput.Player.Enable();
        _gameInput.UI.Enable();
    }

    public event Action<InputAction.CallbackContext> OnInputAbility0;
    public event Action<InputAction.CallbackContext> OnInputAbility1;
    public event Action<InputAction.CallbackContext> OnInputAbility2;
    public event Action<InputAction.CallbackContext> OnInputAbility3;
    public event Action<InputAction.CallbackContext> OnInputAbility4;
    public event Action<InputAction.CallbackContext> OnUsePotion;

    public event Action<Vector2> OnInputMousePosition;
    public event Action<InputAction.CallbackContext> OnInputLeftMouse;

    public event Action OnInputOpenMenu;
    public event Action OnInputOpenSkills;
    public void OnPotion(InputAction.CallbackContext context)
    {
        OnUsePotion?.Invoke(context);
    }

    public void OnSecondaryAbility(InputAction.CallbackContext context)
    {
       OnInputAbility0?.Invoke(context);
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        OnInputAbility1?.Invoke(context);
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        OnInputAbility2?.Invoke(context);
    }

    public void OnAbility3(InputAction.CallbackContext context)
    {
        OnInputAbility3?.Invoke(context);
    }

    public void OnAbility4(InputAction.CallbackContext context)
    {
        OnInputAbility4?.Invoke(context);
    }

    public void OnLeftMouseB(InputAction.CallbackContext context)
    {
        OnInputLeftMouse?.Invoke(context);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        OnInputMousePosition?.Invoke(context.ReadValue<Vector2>());
    }


    public void OnOpenMenu(InputAction.CallbackContext context)
    {
        if(context.phase==InputActionPhase.Started)OnInputOpenMenu?.Invoke();
    }

    public void OnSkillTrees(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) OnInputOpenSkills?.Invoke();
    }
}
