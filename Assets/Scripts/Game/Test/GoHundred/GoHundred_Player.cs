using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoHundred_Player : MonoBehaviour
{
    public float moveSpeed;  //ÒÆ¶¯ËÙ¶È

    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3( moveSpeed , rb.velocity.y, 0);
            //transform.position = new Vector3(transform.position.x + 5*Time.deltaTime,transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);
        }
    }
}
