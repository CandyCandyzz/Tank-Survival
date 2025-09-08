using UnityEngine;

public class DragonIdelState : IState
{
    private DragonController dragon;
    private Animator animator;
    private StateManager stateManager;
    public DragonIdelState(DragonController dragon)
    {
        this.dragon = dragon;
        animator = dragon.animator; 
        stateManager = dragon.stateManager;
    }

    public void Enter()
    {
        animator.SetBool("isIdle", true);
    }

    public void Excute()
    {
        if(dragon.IsEndIdle())
        {
            stateManager.ChangeState(dragon.moveState);
        }
    }

    public void Exit()
    {
        animator.SetBool("isIdle", false);
    }
}
