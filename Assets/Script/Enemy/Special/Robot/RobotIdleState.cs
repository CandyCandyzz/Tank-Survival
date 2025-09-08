using UnityEngine;

public class RobotIdleState : IState
{
    private RobotController controller;
    private StateManager stateManager;

    public RobotIdleState(RobotController controller, StateManager stateManager)
    {
        this.controller = controller;
        this.stateManager = stateManager;
    }

    public void Enter()
    {
        controller.animator.SetBool("isIdle", true);
    }

    public void Excute()
    {
        if (controller.IsEndIdle())
        {
            stateManager.ChangeState(controller.moveState);
        }
    }

    public void Exit()
    {
        controller.animator.SetBool("isIdle", false);
    }
}
