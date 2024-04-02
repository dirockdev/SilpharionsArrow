using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationHandler : MonoBehaviour
{
    PlayerCharacterInput characterInput;
    CharacterMovInput movInput;
    NavMeshAgent agent;
    [SerializeField]
    ParticleSystem partTeleport;

    private Vector3 teleportPos;
    private void Awake()
    {
        movInput = GetComponentInParent<CharacterMovInput>();   
        characterInput = GetComponentInParent<PlayerCharacterInput>();
        agent = GetComponentInParent<NavMeshAgent>();
    }

    public void CanMove()
    {
        characterInput.canMove = !characterInput.canMove;
        movInput.StopInput();
    }
    public void Teleport()
    {
        agent.Warp(teleportPos);
       
    }

    public void TeleportBurst(int movSpeed,int timeSpeedBurst)
    {
        StartCoroutine(SpeedBurst(movSpeed, timeSpeedBurst));
    }
    IEnumerator SpeedBurst(int speedAmount, int timeBurst)
    {
        agent.speed += speedAmount;
        yield return Yielders.Get(timeBurst);
        agent.speed -= speedAmount;
    }

    public void PartAnim()
    {
        teleportPos = MouseInput.rayToWorldPoint;
        partTeleport.Play();
    }
}
