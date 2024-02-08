using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //AI, 네비게이션 시스템 관련 코드 가져오기

public class NormalMonster : Monster
{
    public LayerMask whatIsTarget; //추적대상 레이어

    public GameObject targetEntity;//추적대상
    private NavMeshAgent pathFinder; //경로 계산 AI 에이전트

    /*public ParticleSystem hitEffect; //피격 이펙트
    public AudioClip deathSound;//사망 사운드
    public AudioClip hitSound; //피격 사운드
    */

    //private Animator enemyAnimator; // 애니메이션
    //private AudioSource enemyAudioPlayer; //오디오 소스 컴포넌트

    public float damage = 20f; //공격력
    public float attackDelay = 1f; //공격 딜레이
    private float lastAttackTime; //마지막 공격 시점
    private float dist; //추적대상과의 거리

    //public Transform tr;

    private float attackRange = 2.3f;

    //추적 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            //추적할 대상이 존재하고, 대상이 사망하지 않았다면 true
            if (targetEntity != null)
            {
                return true;
            }

            //그렇지 않다면 false
            return false;
        }
    }

    private bool canMove;
    private bool canAttack;

    private void Awake()
    {
        //게임 오브젝트에서 사용할 컴포넌트 가져오기
        pathFinder = GetComponent<NavMeshAgent>();
        //enemyAnimator = GetComponent<Animator>();
        //enemyAudioPlayer = GetComponent<AudioSource>();
    }

    //적 AI의 초기 스펙을 결정하는 셋업 메서드
    public void Setup(float newHealth, float newDamage, float newSpeed)
    {
        //체력 설정
        startingHealth = newHealth;
        health = newHealth;
        //공격력 설정
        damage = newDamage;
        //네비메쉬 에이전트의 이동 속도 설정
        pathFinder.speed = newSpeed;
    }


    void Start()
    {
        //게임 오브젝트 활성화와 동시에 AI의 탐지 루틴 시작
        //StartCoroutine(UpdatePath());
        //tr = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //enemyAnimator.SetBool("CanMove", canMove);
        //enemyAnimator.SetBool("CanAttack", canAttack);

        if (hasTarget)
        {
            //추적 대상이 존재할 경우 거리 계산은 실시간으로 해야하니 Update()
            dist = Vector3.Distance(transform.position, targetEntity.transform.position);
            pathFinder.SetDestination(targetEntity.transform.position);
            
        }
        Attack();
    }

    //추적할 대상의 위치를 주기적으로 찾아 경로 갱신
    /*
    private IEnumerator UpdatePath()
    {
        //살아 있는 동안 무한 루프
        while (!dead)
        {
            if (hasTarget)
            {
                Attack();
            }
            else
            {
                //추적 대상이 없을 경우, AI 이동 정지
                pathFinder.isStopped = true;
                canAttack = false;
                canMove = false;

                //반지름 20f의 콜라이더로 whatIsTarget 레이어를 가진 콜라이더 검출하기
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIsTarget);

                //모든 콜라이더를 순회하면서 살아 있는 LivingEntity 찾기
                for (int i = 0; i < colliders.Length; i++)
                {
                    //콜라이더로부터 LivingEntity 컴포넌트 가져오기
                    Monster livingEntity = colliders[i].GetComponent<Monster>();

                    //LivingEntity 컴포넌트가 존재하며, 해당 LivingEntity가 살아 있다면
                    if (livingEntity != null && !livingEntity.dead)
                    {
                        //추적 대상을 해당 LivingEntity로 설정
                        targetEntity = livingEntity;

                        //for문 루프 즉시 정지
                        break;
                    }
                }
            }

            //0.25초 주기로 처리 반복
            yield return new WaitForSeconds(0.25f);
        }
    }
    */

    //추적 대상과의 거리에 따라 공격 실행
    public virtual void Attack()
    {

        //자신이 사망X, 추적 대상과의 거리이 공격 사거리 안에 있다면
        if (!dead && dist <= attackRange)
        {
            //공격 반경 안에 있으면 움직임을 멈춘다.
            canMove = false;

            //추적 대상 바라보기
            this.transform.LookAt(targetEntity.transform);

            //최근 공격 시점에서 attackDelay 이상 시간이 지나면 공격 가능
            if (lastAttackTime + attackDelay <= Time.time)
            {
                canAttack = true;
            }

            //공격 반경 안에 있지만, 딜레이가 남아있을 경우
            else
            {
                canAttack = false;
            }
            pathFinder.velocity = Vector3.zero;
            Debug.Log("attack");
        }

        //공격 반경 밖에 있을 경우 추적하기
        else
        {
            canMove = true;
            canAttack = false;
            //계속 추적
            pathFinder.isStopped = false; //계속 이동
            pathFinder.SetDestination(targetEntity.transform.position);
        }
    }

    //유니티 애니메이션 이벤트로 휘두를 때 데미지 적용시키기
    public void OnDamageEvent()
    {
        //공격 대상을 지정할 추적 대상의 LivingEntity 컴포넌트 가져오기
        Monster attackTarget = targetEntity.GetComponent<Monster>();

        //공격 처리
        attackTarget.OnDamage(damage);

        //최근 공격 시간 갱신
        lastAttackTime = Time.time;
    }


    //데미지를 입었을 때 실행할 처리
    public override void OnDamage(float damage)
    {
        /*사망하지 않을 상태에서만 피격 효과 재생
        if (!dead)
        {
            //공격 받은 지점과 방향으로 피격 효과 재생
            hitEffect.transform.position = hitPoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            //피격 효과음 재생
            enemyAudioPlayer.PlayOnShot(hitSound);
        }
        */

        //피격 애니메이션 재생
        //enemyAnimator.SetTrigger("Hit");


        //LivingEntity의 OnDamage()를 실행하여 데미지 적용
        base.OnDamage(damage);
    }

    //사망 처리
    public override void Die()
    {
        //LivingEntity의 DIe()를 실행하여 기본 사망 처리 실행
        base.Die();

        //다른 AI를 방해하지 않도록 자신의 모든 콜라이더를 비활성화
        Collider[] enemyColliders = GetComponents<Collider>();
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].enabled = false;
        }

        //AI추적을 중지하고 네비메쉬 컴포넌트를 비활성화
        pathFinder.isStopped = true;
        pathFinder.enabled = false;

        //사망 애니메이션 재생
        //enemyAnimator.SetTrigger("Die");
        /*//사망 효과음 재생
        enemyAudioPlayer.PlayOnShot(deathSound);
        */
    }
}