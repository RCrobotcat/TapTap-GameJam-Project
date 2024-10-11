using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    public Vector3 trans;     //��������
    private  GameObject target;    //��ȡ�������

    [Header("��������")]
    public float chaseRange;        //��ⷶΧ
    public Vector3 randomPosition;
    [Header("״̬")]
    public bool isChasing;          //�Ƿ���׷��
    public bool isPatrol;           //�Ƿ���Ѳ��
    public bool isArrive;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player");
        trans = gameObject.GetComponent<Transform>().position;
    }

    private void Update()
    {
        if (isPatrol)
        {
            OnPatrol();
        }
        OnChase();

    }

    private void OnPatrol()
    {

        if (isArrive)
        {
            randomPosition = new Vector3(trans.x + UnityEngine.Random.Range(-5f, 5f), 0f , trans.z + UnityEngine.Random.Range(-5f,5f));
            isArrive = false;
        }
        if ((transform.position.x != randomPosition.x) && (transform.position.z != randomPosition.z))
        {
            agent.SetDestination(randomPosition);

        }
        else {
            isArrive = true;
        }
    }

    private void OnChase()
    {
        float distanceToPlayer = Vector3.Distance(target.transform.position, transform.position);
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            isPatrol = false;
        }
        else {
            isChasing = false;
        }

        if (isChasing)
        {
            agent.SetDestination(target.transform.position);
        }

        if (!isChasing && !isPatrol)
        {
            agent.SetDestination(trans);
            if (new Vector3(transform.position.x,0f,transform.position.z) == new Vector3(trans.x,0f,trans.z))
            {
                Debug.Log(1);
                isPatrol = true;
                isArrive = true;
            }
            
        }
    }

    private void OnDrawGizmosSelected() //���ƾ���뾶
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position ,chaseRange);
    }
}
