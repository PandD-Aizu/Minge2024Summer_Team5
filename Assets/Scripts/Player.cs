using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 12f;
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
        // 入力の取得
        float moveHorizontal = Input.GetAxis("Horizontal");  // 左右の入力 (A, D)

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 移動ベクトルの計算
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        // Rigidbodyを使用して移動
        rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
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

