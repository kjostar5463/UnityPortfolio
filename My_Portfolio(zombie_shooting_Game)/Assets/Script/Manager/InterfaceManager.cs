using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamageable
{
    void TakeHit(float damage, GameObject clone);
}

public interface IState
{
    void Action(Animator animator, NavMeshAgent navMeshAgent);
}