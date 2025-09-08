using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class RobotController : MonoBehaviour
{
    public Transform player;

    public StateManager stateManager = new();
    public Animator animator;
    [SerializeField] private CharacterStat stat;

    private ObjectPooling enemyPool;
    [SerializeField] private EnemyTag enemyTag;

    [Header("NavMeshAgent")]
    public NavMeshAgent navMeshAgent;
    [SerializeField] private float timeSet;
    private float time = 0;


    [Header("LaserAtk")]
    [SerializeField] private float rangeAtk;
    [SerializeField] private Transform[] firePoints;
    [SerializeField] private float maxLaserDist;
    public bool canAtk = false;
    public bool isEndAtk = false;

    private ObjectPooling laserPool;
    [SerializeField] private string objTag;
    private LaserShooter shooter;
    [SerializeField] private UnityEvent onAtk;


    [Header("Idle")]
    [SerializeField] private float timeRest;
    private float timer = 0;

    [Header("State")]
    public RobotMoveState moveState; 
    public RobotAtkState atkState;
    public RobotIdleState idleState;
    private DeathState deathState;


    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        enemyPool = transform.parent.GetComponent<ObjectPooling>();

        laserPool = GameObject.Find("BulletPool").GetComponent<ObjectPooling>();
        shooter = new(laserPool, objTag, maxLaserDist, firePoints, stat.atk.GetValue());

        moveState = new(this, stateManager);
        atkState = new(this, stateManager);
        idleState = new(this, stateManager);
        deathState = new(animator, enemyPool, enemyTag.ToString());

        stateManager.ChangeState(moveState);
    }

    public void FixedUpdate()
    {
        stateManager.currentState.Excute();
    }

    public void Move()
    {
        if (player == null) { return; }

        time += Time.deltaTime;
        if (time < timeSet) { return; }
        navMeshAgent.SetDestination(player.position);
        time = 0;
    }

    public void Die()
    {
        stateManager.ChangeState(deathState);
    }

    public bool InRangeAtk()
    {
        if (player == null) { return false; }
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= rangeAtk)
        {
            return true;
        }
        return false;
    }

    public void LookAtPlayer()
    {
        if (player == null) { return; }
        Vector3 dir = (player.position - transform.position).normalized;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    public void CanAtk() //AniEvent
    {
        canAtk = true;
    }

    public void Attack()
    {
        if (player == null) { return; }
        onAtk.Invoke();
        shooter.Shoot(player.position + Vector3.up, "Player");
    }

    public void IsEndAtk() //AniEvent
    {
        isEndAtk = true;
    }

    public bool IsEndIdle()
    {
        timer += Time.deltaTime;
        if (timer >= timeRest)
        {
            timer = 0;
            return true;
        }
        return false;
    }
}
