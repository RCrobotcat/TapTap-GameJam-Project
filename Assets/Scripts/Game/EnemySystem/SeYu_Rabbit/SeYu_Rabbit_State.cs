using UnityEngine;

public class SeYu_Rabbit_State : MonoBehaviour
{ }

public class SeYu_Rabbit_PatrolState : EnemyBaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.isArrive)
        {
            //��ȡ�µ�����
            currentEnemy.randomPosition = new Vector3(currentEnemy.trans.x + Random.Range(-5f, 5f), 0f, currentEnemy.trans.z + Random.Range(-5f, 5f));
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

    public override void PhysicsUpdate()
    {
        //������ת
        /*if (currentEnemy.transform.position.x - currentEnemy.randomPosition.x < 0f)
        {
            currentEnemy.transform.localScale = new Vector3(-0.3f, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if (currentEnemy.transform.position.x - currentEnemy.randomPosition.x > 0f)
        {
            currentEnemy.transform.localScale = new Vector3(0.3f, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }*/
    }
    public override void OnExit()
    {
        //TODD: �����ر�
    }
}

public class SeYu_Rabbit_ChaseState : EnemyBaseState
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

        //�л�״̬
        if (currentEnemy.isAttack)
        {
            currentEnemy.SwitchState(EnemyState.Attackiing);
        }
    }

    public override void PhysicsUpdate()
    {
        //������ת
        /*if (currentEnemy.transform.position.x - currentEnemy.target.transform.position.x > 0f)
        {
            currentEnemy.transform.localScale = new Vector3(0.3f, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }
        if (currentEnemy.transform.position.x - currentEnemy.target.transform.position.x < 0f)
        {
            currentEnemy.transform.localScale = new Vector3(-0.3f, currentEnemy.transform.localScale.y, currentEnemy.transform.localScale.z);
        }*/
    }

    public override void OnExit()
    {
        //TODD : �����ر�
    }
}

public class SeYu_Rabbit_AttackState : EnemyBaseState
{

    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
    }
    public override void LogicUpdate()
    {
        if (currentEnemy.FoundPlayer())
        {
            if (currentEnemy.beAttacking)
            {
                //���ɵ�Ļ
                currentEnemy.attackPosition = GameObject.FindWithTag("Player").transform.position;
                Instantiate(currentEnemy.MagicPrefab, new Vector3(currentEnemy.attackPosition.x, 0.5f, currentEnemy.attackPosition.z), Quaternion.identity);

                currentEnemy.beAttacking = false;
                currentEnemy.MagicAttackCounter = 0f;
            }
        }
        if (!currentEnemy.isAttack && !currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(EnemyState.Patroling);
        }
    }

    public override void PhysicsUpdate() { }
    public override void OnExit() { }
}
