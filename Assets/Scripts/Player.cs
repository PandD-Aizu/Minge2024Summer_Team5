using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public int jumppower = 250; //ジャンプする力
    public bool isGround;
    public GameObject respornPoint;
    private Rigidbody rb;

    void Start()
    {
        respornPoint = GameObject.Find("respornPoint");
        rb = GetComponent<Rigidbody>();
        isGround = false;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            //movement += new Vector3(-1, 0, 0); 左 (X軸方向)
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            //movement += new Vector3(1, 0, 0);  右 (X軸方向)
            //movement += new Vector3(1, 0, 0);  右 (X軸方向)
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
    public void Jump()
    {
        if (isGround == true)
        {
            rb.AddForce(new Vector3(0, jumppower, 0));
        }
    }

    public void playerResporn() {
        rb.velocity = new Vector3(0, 0, 0);
        this.transform.position = respornPoint.transform.position;
    }
}

