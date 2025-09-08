using UnityEngine;
using UnityEngine.Events;

public class DragonController : MonoBehaviour, IEnemyFly, IEnemyAttack, IEnemyIdle
{
    [SerializeField] private Rigidbody rb;
    
    public Animator animator;
    public StateManager stateManager = new();
    [SerializeField] private CharacterStat stat;
    private ObjectPooling enemyPool;
    [SerializeField] private EnemyTag enemyTag;
    private Transform player;

    [Header("Fly")]
    [SerializeField] private float flySpeed;
    [SerializeField] private float maxHeight;
    private BasicFly fly;

    [Header("Attack")]
    [SerializeField] private UnityEvent onAtk;
    [SerializeField] private float rangeAtk;
    [SerializeField] private int fireBallCount;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootAngle;
    [SerializeField] private float fireBallSpeed;
    public bool canAtk = false;
    public bool isEndAtk = false;
    private ObjShooter shooter;
    private ObjectPooling fireballPool;
    [SerializeField] private string objTag;

    [Header("Idle")]
    [SerializeField] private float timeRest;
    private float timer = 0;

    [Header("State")]
    public DragonMoveState moveState;
    public DragonAttackState attackState;
    public DragonIdelState idelState;
    public DeathState deathState;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyPool = transform.parent.GetComponent<ObjectPooling>();

        fly = new(player, rb, flySpeed, maxHeight);
        fireballPool = GameObject.Find("BulletPool").GetComponent<ObjectPooling>();
        shooter = new(fireballPool, fireBallCount, firePoint, shootAngle, fireBallSpeed, stat.atk.GetValue(), objTag);

        moveState = new(this);
        attackState = new(this);
        idelState = new(this);
        deathState = new(animator, enemyPool, enemyTag.ToString());

        stateManager.ChangeState(moveState);
    }

    private void FixedUpdate()
    {
        stateManager.currentState.Excute();
    }

    public void Fly()
    {
        if(player == null) { return; }
        fly.PerformFly();
    }

    #region Attack
    public bool InRangeAttack()
    {
        if (player == null) { return false; }
        float distance = Vector3.Distance(rb.position, player.position);
        if (distance <= rangeAtk)
        {
            return true;
        }
        return false;
    }

    public void CanAtk() //AniEvent
    {
        canAtk = true;
    }

    public void Attack()
    {
        if (player == null) { return; }
        onAtk.Invoke();
        shooter.Shot(player.position + Vector3.up, "Player", "Enemy");
    }

    public void IsEndAtk() //AniEvent
    {
        isEndAtk = true;
    }
    #endregion

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

    public void Die()
    {
        stateManager.ChangeState(deathState);
    }
}
