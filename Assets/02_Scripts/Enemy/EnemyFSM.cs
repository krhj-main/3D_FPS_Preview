using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EnemyState
{
    Idle,
    Move,
    Attack,
    Return,
    Hitted,
    Death,
}
public class EnemyFSM : MonoBehaviour
{
    EnemyState enemyState;

    // ������ Ž���� �ִ�Ÿ�
    public float findDistance = 8f;
    // �̵��ӵ�
    public float moveSpeed = 5f;
    // ���ݰ��� �Ÿ�
    public float attackDistance = 2f;
    CharacterController cc;

    Transform player;

    // ���� ����
    float currentTime = 0;
    float attackDelay = 2f;
    [SerializeField]public int attackDamage = 1;

    // ��� ����
    Vector3 oriPos;
    Quaternion oriRot;
    public float limitDistance = 20f;


    // ü�°���
    int hp;
    public int maxHp = 15;
    public Slider hpSlider;


    Animator anim;
    float waitCount = 0;
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.Idle;
        anim = GetComponentInChildren<Animator>();

        cc = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        oriPos = transform.position;
        oriRot = transform.rotation;

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = (float)hp / (float)maxHp;
        dist = Vector3.Distance(transform.position, player.position);
        if (GameManager.gm.gState != GameManager.GameState.Run) return;
        switch (enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;

                /*
            case EnemyState.Hitted:
                Hitted();
                break;
            case EnemyState.Death:
                Death();
                break;
                */
        }
    }

    void Idle()
    {
        if (findDistance > dist)
        {
            anim.SetTrigger("IdleToMove");
            enemyState = EnemyState.Move;
        }
    }
    void Move()
    {
        Vector3 _dir = (player.position - transform.position).normalized;
        float _dist = Vector3.Distance(oriPos, transform.position);

        if (dist > attackDistance)
        {
            cc.SimpleMove(moveSpeed * _dir);
            transform.forward = _dir;
        }
        else
        {
            enemyState = EnemyState.Attack;
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
        }

        

        if (_dist > limitDistance)
        {
            enemyState = EnemyState.Return;
        }
    }
    void Attack()
    {
        if (dist < attackDistance)
        {
            // �÷��̾���� �Ÿ��� ���� �����ǿ� �ִٸ� ����
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                currentTime = 0;
                // �÷��̾� �����Լ� ȣ��
                // HitEvent ��ũ��Ʈ���� ȣ��
                //player.GetComponent<PlayerMove>().Damaged(attackDamage);
                anim.SetTrigger("StartAttack");
            }
        }
        else
        {
            // �ƴ϶�� �ٽ� ����
            enemyState = EnemyState.Move;
            currentTime = 0;
            anim.SetTrigger("AttackToMove");
        }
    }
    public void AttackAction()
    {
        player.GetComponent<PlayerMove>().Damaged(attackDamage);
    }

    void Return()
    {
        float _dist = Vector3.Distance(oriPos, transform.position);
        if (_dist > 0.2f)
        {
            Vector3 _dir = (oriPos - transform.position).normalized;
            cc.SimpleMove(_dir* moveSpeed);
            transform.forward = _dir;
        }
        else
        {
            transform.position = oriPos;
            transform.rotation = oriRot;
            anim.SetTrigger("MoveToIdle");
            hp = maxHp;
            enemyState = EnemyState.Idle;
        }
    }
    void Hitted()
    {
        StartCoroutine(DamageProcess());
    }
    IEnumerator DamageProcess()
    {
        //anim.SetBool("Hitted",true);
        // ��� ����
        yield return new WaitForSeconds(1f);
        //anim.SetBool("Hitted",false);
        // ���� ���� ����
        enemyState = EnemyState.Move;

    }
    public void Damaged(int _damaged)
    {
        if (enemyState == EnemyState.Hitted || enemyState == EnemyState.Death || enemyState == EnemyState.Return)
            return;

        hp -= _damaged;

        if (hp > 0)
        {
            anim.SetTrigger("Damaged");
            enemyState = EnemyState.Hitted;
            Hitted();
        }
        else
        {
            anim.SetTrigger("Death");
            enemyState = EnemyState.Death;
            Death();
        }
    }
    void Death()
    {
        StopAllCoroutines();

        StartCoroutine(DeathProcess());
    }
    IEnumerator DeathProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
