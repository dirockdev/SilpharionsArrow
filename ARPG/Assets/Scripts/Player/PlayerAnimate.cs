using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimate : MonoBehaviour
{

    private Animator anim;
    private const string runMotion = "motion";
    NavMeshAgent agent;
    public ParticleSystem partParent;
    [SerializeField]
    private AudioSource footSteps;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    public void LevelUpPart()
    {
        partParent.Play();
    }
    void Update()
    {
        float motion = agent.velocity.magnitude;
        anim.SetFloat(runMotion, motion);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            footSteps.enabled=false;
        }
        else
        {
            footSteps.enabled=true;
        }
    }
}
