using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class SpiderController : MonoBehaviour, IEnemyMove, IEnemyJump, IEnemyIdle
{
    private Transform player;

    public StateManager stateManager = new();
    public Animator animator;

    private ObjectPooling enemyPool;
    [SerializeField] private EnemyTag enemyTag;

    [Header("Move")]
    public NavMeshAgent navMeshAgent;
    [SerializeField] private float moveSpeed;
    public float timeSet = 0;


    [Header("Jump")]
    [SerializeField] private float timeDur;
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxRangeJump;
    [SerializeField] private float minRangeJump;
    private JumpToTarget jump;
    private bool canPerformJump = false;
    private bool isJumping = false;
    [SerializeField] private UnityEvent onAtk;

    [Header("Idle")]
    public float timeRest;
    public float time = 0;

    [Header("State")]
    public SpiderMoveState moveState;
    public SpiderJumpState jumpState;
    public SpiderIdleState idleState;
    public DeathState deathState;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        enemyPool = transform.parent.GetComponent<ObjectPooling>();

        jump = new(transform, player, timeDur, maxHeight);

        moveState = new(this);
        jumpState = new(this);
        idleState = new(this);
        deathState = new(animator, enemyPool, enemyTag.ToString());

        stateManager.ChangeState(moveState);
    }

    private void FixedUpdate()
    {
        stateManager.currentState.Excute();
    }

    public void Move()
    {
        if (player == null) { return; }

        timeSet += Time.deltaTime;
        if (timeSet < 0.5f) { return; }
        timeSet = 0;
        navMeshAgent.SetDestination(player.position);
    }

    #region Jump
    public bool InRangeJump()
    {
        if (player == null) { return false; }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < maxRangeJump && distance > minRangeJump)
        {
            return true;
        }
        return false;
    }

    public void LookAtPlayer()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void PreJump()
    {
        jump.PreJump();
    }

    public void CanPerformJump()
    {
        canPerformJump = true;
    }

    public void PerformJump()
    {
        if (!canPerformJump) { return; }
        if (!isJumping)
        {
            onAtk.Invoke();
            isJumping = true;
        }

        jump.PerformJump();
    }

    public bool IsEndJump()
    {
        return jump.isEndJump;
    }

    public void EndJump()
    {
        isJumping = false;
        jump.EndJump();
        canPerformJump = false;
    }
    #endregion

    public bool IsEndIdle()
    {
        time += Time.deltaTime;
        if(time >= timeRest)
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        stateManager.ChangeState(deathState);
    }
}
