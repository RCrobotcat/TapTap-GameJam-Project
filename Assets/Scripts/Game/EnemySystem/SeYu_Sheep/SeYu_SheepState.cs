using UnityEngine;

public class SeYu_PatrolState : EnemyBaseState    //Ñ²Âß×´Ì¬
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }

    public override void LogicUpdate()
    {
        //Ö´ÐÐÑ²Âß·½·¨
        if (currentEnemy.isArrive)
        {
            //»ñÈ¡ÐÂµÄ×ø±ê
            currentEnemy.randomPosition = new Vector3(currentEnemy.trans.x + Random.Range(-3f, 3f), 0f, currentEnemy.trans.z + Random.Range(-3f, 3f));
            currentEnemy.isArrive = false;
        }
        if ((currentEnemy.transform.position.x != currentEnemy.randomPosition.x) && (currentEnemy.transform.position.z != currentEnemy.randomPosition.z))
        {
            //Ç°ÍùÐÂµÄ×ø±ê
            currentEnemy.agent.SetDestination(currentEnemy.randomPosition);
        }
        else
        {
            currentEnemy.isArrive = true;
        }

        //ÇÐ»»×´Ì¬
        if (currentEnemy.FoundPlayer())
        {
            //Ò»µ©ÕÒµ½Íæ¼Ò¾ÍÇÐ»»×´Ì¬
            currentEnemy.SwitchState(EnemyState.Chasing);
        }
    }

    public override void PhysicsUpdate() { }

    public override void OnExit()
    {
        //TODD: ¹Ø±Õ¶¯»­
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
        //×·»÷Íæ¼Ò
        currentEnemy.agent.SetDestination(currentEnemy.target.transform.position);

        //ÇÐ»»×´Ì¬
        if (!currentEnemy.FoundPlayer())
        {
            //ÇÐ»»ÎªÑ²Âß×´Ì¬
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
