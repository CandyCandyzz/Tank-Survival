using UnityEngine;
using UnityEngine.AI;

public class SpiderMoveState : IState
{
    private SpiderController spider;
    private StateManager stateManager;

    public SpiderMoveState(SpiderController spider)
    {
        this.spider = spider;
        stateManager = this.spider.stateManager;
    }

    public void Enter()
    {
        spider.navMeshAgent.Warp(spider.transform.position);
        spider.navMeshAgent.updatePosition = true;
        spider.navMeshAgent.updateRotation = true;
    }

    public void Excute()
    {
        spider.Move();
        if(spider.InRangeJump())
        {
            stateManager.ChangeState(spider.jumpState);
        }
    }

    public void Exit()
    {
        spider.navMeshAgent.updatePosition = false;
        spider.navMeshAgent.updateRotation = false;
    }
}
