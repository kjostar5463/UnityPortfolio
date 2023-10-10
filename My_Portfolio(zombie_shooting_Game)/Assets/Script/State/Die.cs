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

        // �ִϸ����� ��Ʈ�ѷ����� ���� �ִϸ������� ������ �̸���"Die���� �� 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Die_Animation"))
        {
            // ���� �ִϸ��̼��� ���൵�� 1���� ũ�ų� ���ٸ� User Interface�� ��Ȱ��ȭ�մϴ�.
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Destroy(animator.gameObject);
            }
        }
    }
}