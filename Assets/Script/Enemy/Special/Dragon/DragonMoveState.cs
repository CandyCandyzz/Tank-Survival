
public class DragonMoveState : IState
{
    private DragonController dragon;
    private StateManager stateManager;

    public DragonMoveState(DragonController dragon)
    {
        this.dragon = dragon;
        stateManager = dragon.stateManager;
    }

    public void Enter()
    {
        
    }

    public void Excute()
    {
        dragon.Fly();
        if(dragon.InRangeAttack())
        {
            stateManager.ChangeState(dragon.attackState);
        }
    }

    public void Exit()
    {
        
    }
}
