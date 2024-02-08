using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ThrowMonster : Monster
{
    public LayerMask whatIsTarget; //������� ���̾�

    public GameObject targetEntity;//�������
    private NavMeshAgent pathFinder; //��� ��� AI ������Ʈ

    public float damage = 20f; //���ݷ�
    public float attackDelay = 1f; //���� ������
    private float lastAttackTime; //������ ���� ����
    private float dist; //���������� �Ÿ�

    //public Transform tr;

    private float attackRange = 10.3f;

    //���� ����� �����ϴ��� �˷��ִ� ������Ƽ
    private bool hasTarget
    {
        get
        {
            //������ ����� �����ϰ�, ����� ������� �ʾҴٸ� true
            if (targetEntity != null)
            {
                return true;
            }

            //�׷��� �ʴٸ� false
            return false;
        }
    }

    private bool canMove;
    private bool canAttack;

    private void Awake()
    {
        //���� ������Ʈ���� ����� ������Ʈ ��������
        pathFinder = GetComponent<NavMeshAgent>();
    }

    //�� AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    public void Setup(float newHealth, float newDamage, float newSpeed)
    {
        //ü�� ����
        startingHealth = newHealth;
        health = newHealth;
        //���ݷ� ����
        damage = newDamage;
        //�׺�޽� ������Ʈ�� �̵� �ӵ� ����
        pathFinder.speed = newSpeed;
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (hasTarget)
        {
            //���� ����� ������ ��� �Ÿ� ����� �ǽð����� �ؾ��ϴ� Update()
            dist = Vector3.Distance(transform.position, targetEntity.transform.position);
            pathFinder.SetDestination(targetEntity.transform.position);

        }
        Attack();
    }

    

    //���� ������ �Ÿ��� ���� ���� ����
    public virtual void Attack()
    {
        //�ڽ��� ���X, ���� ������ �Ÿ��� ���� ��Ÿ� �ȿ� �ִٸ�
        if (!dead && dist <= attackRange)
        {
            //���� �ݰ� �ȿ� ������ �������� �����.
            canMove = false;

            //���� ��� �ٶ󺸱�
            this.transform.LookAt(targetEntity.transform);

            //�ֱ� ���� �������� attackDelay �̻� �ð��� ������ ���� ����
            if (lastAttackTime + attackDelay <= Time.time)
            {
                canAttack = true;
            }

            //���� �ݰ� �ȿ� ������, �����̰� �������� ���
            else
            {
                canAttack = false;
            }
            pathFinder.velocity = Vector3.zero;
            Debug.Log("throw");
        }

        //���� �ݰ� �ۿ� ���� ��� �����ϱ�
        else
        {
            canMove = true;
            canAttack = false;
            //��� ����
            pathFinder.isStopped = false; //��� �̵�
            pathFinder.SetDestination(targetEntity.transform.position);
        }
    }

    //����Ƽ �ִϸ��̼� �̺�Ʈ�� �ֵθ� �� ������ �����Ű��
    public void OnDamageEvent()
    {
        //���� ����� ������ ���� ����� LivingEntity ������Ʈ ��������
        Monster attackTarget = targetEntity.GetComponent<Monster>();

        //���� ó��
        attackTarget.OnDamage(damage);

        //�ֱ� ���� �ð� ����
        lastAttackTime = Time.time;
    }


    //�������� �Ծ��� �� ������ ó��
    public override void OnDamage(float damage)
    {
        
        base.OnDamage(damage);
    }

    //��� ó��
    public override void Die()
    {
        //LivingEntity�� DIe()�� �����Ͽ� �⺻ ��� ó�� ����
        base.Die();

        //�ٸ� AI�� �������� �ʵ��� �ڽ��� ��� �ݶ��̴��� ��Ȱ��ȭ
        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        //AI������ �����ϰ� �׺�޽� ������Ʈ�� ��Ȱ��ȭ
        pathFinder.isStopped = true;
        pathFinder.enabled = false;

    }
}
