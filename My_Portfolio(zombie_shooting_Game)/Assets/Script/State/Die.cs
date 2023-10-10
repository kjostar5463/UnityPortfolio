using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Die : MonoBehaviour, IState
{
    public void Action(Animator animator, NavMeshAgent navMeshAgent)
    {
        navMeshAgent.speed = 0f;
        navMeshAgent.acceleration = 0f;
        animator.Play("Die_Animation");

        // 애니메이터 컨트롤러에서 현재 애니메이터의 상태의 이름이"Die”일 때 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die_Animation"))
        {
            // 현재 애니메이션의 진행도가 1보다 크거나 같다면 User Interface를 비활성화합니다.
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(animator.gameObject);
            }
        }
    }
}