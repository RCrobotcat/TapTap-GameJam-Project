using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float moveSpeed;   //台阶移动速度
    public float deadZone = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.up * moveSpeed)* Time.deltaTime;
        if (transform.position.y > deadZone)
        {
            Destroy(gameObject);
        }
    }
}
