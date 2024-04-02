using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    PlayerCharacterInput characterInput;
    private void Awake()
    {
        characterInput = GetComponentInParent<PlayerCharacterInput>();
    }
    public void CanMove(bool activar)
    {
        characterInput.canMove = activar;
    }
}
