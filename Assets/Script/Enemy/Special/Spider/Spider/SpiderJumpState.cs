using UnityEngine;

public class SpiderJumpState : IState
{
    private SpiderController spider;
    private Animator animator;
    private StateManager stateManager;

    public SpiderJumpState(SpiderController spider)
    {
        this.spider = spider;
        animator = this.spider.animator;
        stateManager = this.spider.stateManager;
    }

    public void Enter()
    {
        spider.LookAtPlayer();
        animator.SetTrigger("isPreJump");
        spider.PreJump();
    }

    public void Excute()
    {
        spider.PerformJump();
        if(spider.IsEndJump())
        {
            stateManager.ChangeState(spider.idleState);
        }    
    }

    public void Exit()
    {
        animator.SetTrigger("isJumpEnd");
        spider.EndJump();
    }
}
