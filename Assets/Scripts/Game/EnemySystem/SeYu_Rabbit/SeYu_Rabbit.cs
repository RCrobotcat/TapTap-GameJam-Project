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
            //��������ͷ�һ�ι������ͻ�ȡҪ����������
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
