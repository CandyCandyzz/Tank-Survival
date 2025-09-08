using UnityEngine;

public class SpiderIdleState : IState
{
    private SpiderController spider;
    private Animator animator;
    private StateManager stateManager;

    public SpiderIdleState(SpiderController spider)
    {
        this.spider = spider;
        animator = this.spider.animator;
        stateManager = this.spider.stateManager;
    }

    public void Enter()
    {
        animator.SetBool("isIdle", true);
    }

    public void Excute()
    {
        if(spider.IsEndIdle())
        {
            stateManager.ChangeState(spider.moveState);
        }
    }

    public void Exit()
    {
        animator.SetBool("isIdle", false);
        spider.time = 0;
    }
}
