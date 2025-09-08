using UnityEngine;

public class RobotMoveState : IState
{
    private RobotController controller;
    private StateManager stateManager;

    public RobotMoveState(RobotController controller, StateManager stateManager)
    {
        this.controller = controller;
        this.stateManager = stateManager;
    }

    public void Enter()
    {
        controller.navMeshAgent.Warp(controller.transform.position);
        controller.transform.position = controller.navMeshAgent.nextPosition;
        controller.navMeshAgent.updatePosition = true;
        controller.navMeshAgent.updateRotation = true;
    }

    public void Excute()
    {
        controller.Move();
        if (controller.InRangeAtk())
        {
            stateManager.ChangeState(controller.atkState);
        }
    }

    public void Exit()
    {
        controller.navMeshAgent.updatePosition = false;
        controller.navMeshAgent.updateRotation = false;
    }
}
