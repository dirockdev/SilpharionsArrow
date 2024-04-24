using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInput : MonoBehaviour
{
    Vector3 mouseInputPosition;
    [HideInInspector]
    public static Vector3 rayToWorldPoint;
    [SerializeField] LayerMask layerTerrain;

    [SerializeField]InputReader inputReader;
    private void Awake()
    {
        inputReader.OnInputMousePosition += MousePositionUpdate;
    }

    private void OnDisable()
    {
        inputReader.OnInputMousePosition -= MousePositionUpdate;
    }
    public void MousePositionUpdate(Vector2 mousePos)
    {
        mouseInputPosition = mousePos;
    }


    void LateUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseInputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, layerTerrain))
        {

            rayToWorldPoint = hit.point;

        }

    }
}

