using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimate : MonoBehaviour
{

    private Animator anim;
    private const string runMotion = "motion";
    NavMeshAgent agent;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float motion = agent.velocity.magnitude;
        anim.SetFloat(runMotion, motion);
    }
}
