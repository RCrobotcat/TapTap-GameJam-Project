using UnityEngine;

public abstract class EnemyBaseState : MonoBehaviour
{
    protected Enemy currentEnemy;

    public abstract void OnEnter(Enemy enemy);

    public abstract void LogicUpdate();

    public abstract void PhysicsUpdate();

    public abstract void OnExit();
}
