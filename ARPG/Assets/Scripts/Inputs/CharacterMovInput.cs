using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovInput : MonoBehaviour
{
    [SerializeField]
    CharacterStats characterMov;
   
    private void Awake()
    {
        characterMov = GetComponent<CharacterStats>();        
    }

    // Update is called once per frame
    public void MoveInput()
    {
        Vector3 direction = (MouseInput.rayToWorldPoint - transform.position).normalized;

        // Calculate the distance to the target
        float distance = Vector3.Distance(transform.position, MouseInput.rayToWorldPoint);

        // Set destination for the NavMeshAgent
        characterMov.SetDestination(transform.position + direction * distance);
        //characterMov.SetDestination(MouseInput.rayToWorldPoint);
    }
    public void StopInput()
    {
        characterMov.SetDestination(transform.position);
    }
}
