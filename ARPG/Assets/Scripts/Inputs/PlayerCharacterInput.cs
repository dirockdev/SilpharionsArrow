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
    private bool canClick = true;

    [SerializeField] GameObject clickEffect;
    [SerializeField] InputReader inputReader;
    public void Awake()
    {
        canMove = true;
        characterMovInput = GetComponent<CharacterMovInput>();
        inputReader.OnInputLeftMouse += LeftMouse;
        GameManager.OnToggleMenu += isMoving;
    }
      private void OnDisable()
    {
        inputReader.OnInputLeftMouse -= LeftMouse;
        
    }

    public void isMoving(bool menu)
    {
        canClick = menu;
    }
  
    private void Update()
    {
        LeftMouseHoldCommand();

    }

    private void LeftMouseHoldCommand()
    {
        if (isPressed && canMove && canClick)
        {
            characterMovInput.MoveInput();
        }
    }

    public void LeftMouse(InputAction.CallbackContext context)
    {

        GameObject partEffect = ObjectPoolManager.SpawnObject(clickEffect, MouseInput.rayToWorldPoint+new Vector3(0,0.2f,0), Quaternion.identity);
        ObjectPoolManager.ReturnToPool(0.4f, partEffect);
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



