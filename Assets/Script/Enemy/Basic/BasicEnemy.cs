using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour, IEnemyMove
{
    public Transform player;

    public StateManager stateManager = new();
    public Animator animator;

    private ObjectPooling enemyPool;
    [SerializeField] private EnemyTag enemyTag;

    [SerializeField] private float moveSpeed;
    public NavMeshAgent navMeshAgent;
    [SerializeField] private float timeSet;
    private float time = 0;

    private BEMoveState moveState;
    private DeathState deathState;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        enemyPool = transform.parent.GetComponent<ObjectPooling>();

        moveState = new(this);
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
        if(time < timeSet) { return; }
        navMeshAgent.SetDestination(player.position);
        time = 0;
    }

    public void Die()
    {
        stateManager.ChangeState(deathState);
    }
}
