using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public enum CharState
{
    Idle,
    Walk,
    WalkToEnemy,
    Attack,
    Hit,
    Die
}


public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent navAgent;

    protected Animator anim;
    public Animator Anim { get { return anim; } }

    [SerializeField]
    protected CharState state;
    public CharState State { get { return state; } }

    [SerializeField] 
    protected GameObject ringSelection;
    public GameObject RingSelection
    {
        get { return ringSelection; }
    }

    [SerializeField] protected int curHp = 10;
    public int CurHP
    {
        get { return curHp; }
    }

    [SerializeField] protected Character curCharTarget;
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected int attackDamage = 3;
    [SerializeField] protected float attackCoolDown = 2f;
    [SerializeField] protected float attackTimer = 0f;
    
    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim =GetComponent<Animator>();
    }

    public void SetState(CharState s)
    {
        state = s;

        if (state == CharState.Idle)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }

    public void WalkToPosition(Vector3 dest)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(dest);
            navAgent.isStopped = false;
        }
        SetState(CharState.Walk);
    }

    protected void WalkUpdate()
    {
        float distance = Vector3.Distance(transform.position, navAgent.destination);
        Debug.Log(distance);

        if (distance <= navAgent.stoppingDistance)
        {
            SetState(CharState.Idle);
        }
    }

    public void ToggleRingSelection(bool flag)
    {
        ringSelection.SetActive(flag);
    }

    public void ToAttackCharacter(Character target)
    {
        if (curHp <= 0 || state == CharState.Die)
        {
            return;
        }
        
        //lock target
        curCharTarget = target;
        //star walking to enemy
        navAgent.SetDestination(target.transform.position);
        navAgent.isStopped = false;
        
        SetState(CharState.WalkToEnemy);
    }

    protected void WalkToEnemyUpdate()
    {
        if (curCharTarget == null)
        {
            SetState(CharState.Idle);
            return;
        }

        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);

        if (distance <= attackRange)
        {
            SetState(CharState.Attack);
            Attack(); //First Attack
        }
    }
    
    protected void Attack()
    {
        transform.LookAt(curCharTarget.transform);
        anim.SetTrigger("Attack");
        //Attack logic
        AttackLogic();
    }

    protected void AttackUpdate()
    {
        if (curCharTarget == null || curCharTarget.CurHP <= 0)
        {
            SetState(CharState.Idle);
            return;
        }

        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {
            attackTimer = 0f;
            Attack();
        }

        float distance = Vector3.Distance(transform.position, curCharTarget.transform.position);
        if (distance > attackRange)
        {
            SetState(CharState.WalkToEnemy);
        }
    }

    protected virtual IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    protected virtual void Die()
    {
        navAgent.isStopped = true;
        SetState(CharState.Die);
        
        anim.SetTrigger("Die");

        StartCoroutine(DestroyObject());
    }

    public void ReceiveDamage(Character enemy)
    {
        if (curHp <= 0 || state == CharState.Die)
        {
           return; 
        }

        curHp -= enemy.attackDamage;
        if (curHp <= 0)
        {
            curHp = 0;
            Die();
        }
    }

    protected void AttackLogic()
    {
        Character target = curCharTarget.GetComponent<Character>();

        if (target != null)
        {
            target.ReceiveDamage(this);
        }
    }

}
