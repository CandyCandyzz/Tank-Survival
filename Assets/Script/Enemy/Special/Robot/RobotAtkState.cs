using UnityEngine;

public class RobotAtkState : IState
{
    private RobotController controller;
    private StateManager stateManager;
    private bool stopTracking = false;

    public RobotAtkState(RobotController controller, StateManager stateManager)
    {
        this.controller = controller;
        this.stateManager = stateManager;
    }

    public void Enter()
    {
        stopTracking = false;
        controller.animator.SetTrigger("isAttack");
    }

    public void Excute()
    {
        if (!stopTracking)
        {
            controller.LookAtPlayer();
        }
        if (controller.canAtk)
        {
            controller.Attack();
            controller.canAtk = false;
            stopTracking = true;
        }
        if (controller.isEndAtk)
        {
            stateManager.ChangeState(controller.idleState);
        }
    }

    public void Exit()
    {
        controller.isEndAtk = false;
    }
}
