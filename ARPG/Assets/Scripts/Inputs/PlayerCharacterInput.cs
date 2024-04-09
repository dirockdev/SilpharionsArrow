using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerCharacterInput : MonoBehaviour
{
    CharacterMovInput characterMovInput;
    [HideInInspector]
    public bool isPressed,canMove;

    private void Awake()
    {
        
        canMove = true;
        characterMovInput = GetComponent<CharacterMovInput>();
    }
    private void Update()
    {
        LeftMouseHoldCommand();

    }

    private void LeftMouseHoldCommand()
    {
        if (isPressed && canMove)
        {
            characterMovInput.MoveInput();
        }
    }

    public void LeftMouse(InputAction.CallbackContext context)
    {
        LeftMouseHold(context);
       
        //characterMovInput.MoveInput();
       

    }

    private void LeftMouseHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPressed = true;
        }
        if (context.canceled)
        {
            isPressed = false;
        }
    }

    
}



