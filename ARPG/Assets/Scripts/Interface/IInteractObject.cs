using UnityEngine;

public interface IInteractObject
{
    string ObjectName();
    int [] Health();
    Outline outLine();
    Vector3 GetPosition();
}