using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    Vector3 mouseInputPosition;
    [HideInInspector]
    public static Vector3 rayToWorldPoint;
    public void MousePositionUpdate(InputAction.CallbackContext context)
    {
        mouseInputPosition=context.ReadValue<Vector2>();
        
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseInputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue))
        {
            rayToWorldPoint = hit.point;
        }

    }
}

