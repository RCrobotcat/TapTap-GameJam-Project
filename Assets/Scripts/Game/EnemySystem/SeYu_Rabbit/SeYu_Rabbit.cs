using UnityEngine;

public class SeYu_Rabbit : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        patrolState = new SeYu_Rabbit_PatrolState();
        chaseState = new SeYu_Rabbit_ChaseState();
        attackState = new SeYu_Rabbit_AttackState();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //如果可以释放一次攻击，就获取要攻击的坐标
            //if (isAttackCounter >= isAttackDuraction)
            //{
            //    attackPosition = GameObject.FindWithTag("Player").transform.position;
            //}
            isAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = false;
        }
    }
}
