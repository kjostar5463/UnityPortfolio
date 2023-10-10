using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Run : IState
{
    public void Action(Animator animator, NavMeshAgent navMeshAgent)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75)
        {
            animator.SetBool("Attack", false);
            navMeshAgent.speed = 3.5f;
        }
        navMeshAgent.acceleration = 1.0f;
        Transform playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        
        navMeshAgent.SetDestination(playerTransform.position);
    }
}