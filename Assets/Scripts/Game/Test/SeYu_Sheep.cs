using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeYu_Sheep : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        patrolState = new SeYu_PatrolState();     //��ʼ��Ѳ��״̬
        chaseState = new SeYu_ChaseState();       //��ʼ��׷��״̬
    }
}
