

public class BEMoveState : IState
{
    public BasicEnemy basicEnemy;

    public BEMoveState(BasicEnemy basicEnemy)
    {
        this.basicEnemy = basicEnemy;
    }

    public void Enter()
    {
        
    }

    public virtual void Excute()
    {
        basicEnemy.Move();
    }

    public void Exit()
    {
        basicEnemy.navMeshAgent.updatePosition = false;
        basicEnemy.navMeshAgent.updateRotation = false;
    }
}
