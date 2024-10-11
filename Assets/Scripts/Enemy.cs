using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    public Vector3 trans;     //怪物坐标
    private  GameObject target;    //获取人物组件

    [Header("基础属性")]
    public float chaseRange;        //检测范围
    public Vector3 randomPosition;
    [Header("状态")]
    public bool isChasing;          //是否发起追击
    public bool isPatrol;           //是否在巡逻
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

    private void OnDrawGizmosSelected() //绘制警戒半径
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position ,chaseRange);
    }
}
