using UnityEngine;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;

public abstract class Unit : MonoBehaviour, IDamageable
{
    // Compoenents
    protected Rigidbody2D rigid;
    protected Animator animator;

    // Base Datas
    [SerializeField]
    protected UnitData data;
    [SerializeField]
    private UnitAIController aiController;

    // BaseStatus
    public int maxHp;               //최대 체력
    public int curHp;               //현재 체력
    public float moveSpeed;         //이동 속도
    public float attackRange;       //공격 사거리
    public float attackRate;        //초당 공격 속도
    public float attackDamage;      //공격력

    // Datas about combat
    protected Transform target;
    protected TimeData attackCooltime;
    public bool isAttacking { get; protected set; } = false;
    private float attackTimer = 0f;
    private bool attackPerformed = false;

    // Datas about move
    protected Queue<Transform> waypoints = new Queue<Transform>();
    protected Transform curWayPoint;

    public virtual void Init(UnitData _data, Transform[] waypointsArr)
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        data = _data;
        InitStatus();
        foreach(Transform tf in waypointsArr)
        {
            waypoints.Enqueue(tf);
        }
        aiController.BuildBT(this);
        attackCooltime = new TimeData(data.baseAttackRate);
    }

    protected void InitStatus()
    {
        maxHp = data.baseMaxHp;
        curHp = maxHp;

        moveSpeed = data.baseMoveSpeed;
        attackRange = data.baseAttackRange;
        attackRate = data.baseAttackRate;
        attackDamage = data.baseAttackDamage;
    }

    private void Update()
    {
        aiController?.EvaluateBT();
        attackCooltime?.DiscountCooltime();
        if(isAttacking)
        {
            ProcessAttack();
        }
    }

    // 유닛 이동 함수
    public virtual bool Move()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            if (curWayPoint == null || (curWayPoint != null && Vector2.Distance(transform.position, curWayPoint.position) <= 0.01f))
            {
                curWayPoint = waypoints.Dequeue();
            }
        }

        if (curWayPoint != null)
        {
            Debug.Log("Moving");
            animator?.SetBool("IsMoving", true);
            transform.position = Vector3.MoveTowards(transform.position, curWayPoint.position, moveSpeed * Time.deltaTime);

            return true;
        }

        Debug.Log("Move fail");
        animator?.SetBool("IsMoving", false);
        return false;
    }

    #region Attack

    /// <summary>
    /// 오브젝트를 중심으로 원형 범위에 모든 콜라이더를 탐색해 
    /// Enemy 태그를 가진 오브젝트 중 가장 가까운 오브젝트를 타겟으로 설정
    /// 타겟이 없다면 null로 설정
    /// </summary>
    /// <returns></returns>
    public virtual bool FindTarget()
    {
        //Debug.Log("find target");

        if(target != null)
        {
            return true;
        }

        // 공격 범위 내의 충돌 판정을 가진 모든 오브젝트를 배열에 저장
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        // 가장 가까운 거리를 저장하기 위한 실수형 변수
        float minDist = float.MaxValue;
        // 가장 가까운 적 오브젝트의 Transform을 저장할 변수
        Transform bestTarget = null;

        // 배열 내 모든 오브젝트를 비교해 가장 거리가 가까운 적 오브젝트 저장 
        foreach (Collider2D collider in colliders)
        {
            // 적이 아닐경우 바로 넘김
            if(collider.CompareTag("Enemy"))
            {
                float dist = Vector2.Distance(transform.position, collider.transform.position);
                if(dist < minDist){
                    minDist = dist;
                    bestTarget = collider.transform;
                }
            }
        }

        // 타겟 설정
        target = bestTarget;
        // 타겟이 있다면 true 반환 없다면 false 반환
        return target != null;
    }

    /// <summary>
    /// 타겟이 공격 사거리 내에 있는지 확인하는 함수
    /// 타겟이 없거나 사거리 내에 없으면 타겟을 null로 설정 후 false 반환
    /// </summary>
    /// <returns></returns>
    public virtual bool IsTargetInRange()
    {
        //Debug.Log("Check target in range");

        if (target == null) return false;
        if (Vector2.Distance(transform.position, target.position) > attackRange)
        {
            target = null;
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 공격 쿨타임 중인지 확인하는 함수
    /// </summary>
    /// <returns></returns>
    public bool IsAttackCooltime()
    {
        //Debug.Log("IsCooltime " + attackCooltime.timerActivated);
        return !attackCooltime.timerActivated;
    }

    /// <summary>
    /// 공격 시작 가상함수
    /// </summary>
    public virtual void StartAttack()
    {
        //Debug.Log("StartAttack");

        if (target == null || !IsTargetInRange()) return;

        isAttacking = true;
        attackTimer = 0f;
        attackPerformed = false;

        // Start Attack animation
        animator.SetTrigger("Attack");
    }

    protected virtual void ProcessAttack()
    {
        attackTimer += Time.deltaTime;

        // after preDelay, performAttack only once.
        if(!attackPerformed && attackTimer > data.preDelay)
        {
            PerformAttack();
            attackPerformed = true;
        }

        // after afterDelay, Finish Attack.
        if(attackTimer >= data.preDelay + data.postDelay)
        {
            FinishAttack();
        }
    }

    protected virtual void PerformAttack()
    {
        if (target != null && IsTargetInRange())
        {
            //Debug.Log("Perform Attack. attackTimer: " + attackTimer);
            if(target.TryGetComponent<IDamageable>(out IDamageable component))
            {
                component.GetDamage(attackDamage);
            }
        }
    }

    protected virtual void FinishAttack()
    {
        //Debug.Log("Finish Attack. attackTimer: " + attackTimer);
        isAttacking = false;
        attackTimer = 0f;
        attackCooltime.StartTimer();
    }
    #endregion Attack

    public virtual bool Idle()
    {
        Debug.Log("Idle");

        animator.SetBool("IsMoving", false);

        return true;
    }

    public virtual void GetDamage(float damage)
    {

    }

    private void OnDrawGizmos()
    {
        Debug.Log("DrawSphere");
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
