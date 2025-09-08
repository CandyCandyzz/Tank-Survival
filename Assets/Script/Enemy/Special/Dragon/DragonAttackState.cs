using UnityEngine;

public class DragonAttackState : IState
{
    private DragonController dragon;
    private Animator animator;
    private StateManager stateManager;
    public DragonAttackState(DragonController dragon)
    {
        this.dragon = dragon;
        stateManager = dragon.stateManager;
        animator = dragon.animator;
    }

    public void Enter()
    {
        animator.SetTrigger("isAttack");
    }

    public void Excute()
    {
        if(dragon.canAtk)
        {
            dragon.Attack();
            dragon.canAtk = false;
        }
        if(dragon.isEndAtk)
        {
            stateManager.ChangeState(dragon.idelState);
        }

    }

    public void Exit()
    {
        dragon.isEndAtk = false;
    }
}
