using UnityEngine;

public class DeathState : IState
{
    private Animator animator;
    private ObjectPooling pool;
    private string enemyTag;

    private float timeReturnPool = 2f;
    private float timer;

    public DeathState(Animator animator, ObjectPooling pool, string enemyTag)
    {
        this.animator = animator;
        this.pool = pool;
        this.enemyTag = enemyTag;
    }

    public void Enter()
    {
        timer = 0;
        animator.SetTrigger("isDeath");
    }

    public void Excute()
    {
        timer += Time.deltaTime;
        if(timer >= timeReturnPool )
        {
            pool.Return(enemyTag, animator.gameObject);
        }
    }

    public void Exit()
    {
        timer = 0;
    }
}
