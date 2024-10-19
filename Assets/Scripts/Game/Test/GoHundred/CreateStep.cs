using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStep : MonoBehaviour
{
    public GameObject step_perfab;
    public float spawnRate;    //生成时间
    public float timer;
    public float RangeOffset;

    private void Start()
    {
        timer = spawnRate;
    }
    private void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else 
        {
            spawnPipe();
            timer = 0;
        }
    }

    private void spawnPipe()
    {
        //float lowerPoint = transform.position.z - RangeOffset;  //创建随机最小值
        //float hightPoint = transform.position.y + RangeOffset;  //创建随机最大值
        Instantiate(step_perfab, new Vector3(UnityEngine.Random.Range(-5 , 5), transform.position.y, transform.position.z),transform.rotation);
    }

    
}
