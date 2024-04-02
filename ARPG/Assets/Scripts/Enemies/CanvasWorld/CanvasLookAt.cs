using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAt : MonoBehaviour
{
    void Update()
    {
        transform.LookAt(Camera.main.transform.localPosition);
        transform.forward = Camera.main.transform.forward;
    }
}
