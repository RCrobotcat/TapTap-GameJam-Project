using UnityEngine;

public class SeYu_PatrolState : EnemyBaseState    //Ѳ��״̬
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        //ִ��Ѳ�߷���
        if (currentEnemy.isArrive)
        {
            //��ȡ�µ�����
            currentEnemy.randomPosition = new Vector3(currentEnemy.trans.x + Random.Range(-3f, 3f), 0f, currentEnemy.trans.z + Random.Range(-3f, 3f));
            currentEnemy.isArrive = false;
        }
        if ((currentEnemy.transform.position.x != currentEnemy.randomPosition.x) && (currentEnemy.transform.position.z != currentEnemy.randomPosition.z))
        {
            //ǰ���µ�����
            currentEnemy.agent.SetDestination(currentEnemy.randomPosition);
        }
        else
        {
            currentEnemy.isArrive = true;
        }

        //�л�״̬
        if (currentEnemy.FoundPlayer())
        {
            //һ���ҵ���Ҿ��л�״̬
            currentEnemy.SwitchState(EnemyState.Chasing);
        }
    }

    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        //TODD: �رն���
    }
}

public class SeYu_ChaseState : EnemyBaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        //׷�����
        currentEnemy.agent.SetDestination(currentEnemy.target.transform.position);

        //�л�״̬
        if (!currentEnemy.FoundPlayer())
        {
            //�л�ΪѲ��״̬
            currentEnemy.agent.SetDestination(currentEnemy.trans);
            if (new Vector3(currentEnemy.transform.position.x, 0f, currentEnemy.transform.position.z) == new Vector3(currentEnemy.trans.x, 0f, currentEnemy.trans.z))
            {
                currentEnemy.SwitchState(EnemyState.Patroling);
            }
        }
    }

    public override void PhysicsUpdate() { }
    public override void OnExit() { }
}
