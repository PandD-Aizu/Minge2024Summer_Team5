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

　　//動作によって表示するオブジェクトを切り替える。モーションの数だけオブジェクトを用意する。
    public GameObject walk;
    public GameObject standing;
    public GameObject jump;

    public bool isJumping = false;//ジャンプ中であるかどうか(isGroundとは違う扱い)

    void Start()
    {
        respornPoint = GameObject.Find("respornPoint");
        rb = GetComponent<Rigidbody>();
        isGround = false;
        isJumping = false;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
            //movement += new Vector3(-1, 0, 0); 左 (X軸方向)

            //左向きに歩くアニメーション
            standingState(false);
            walkState(true);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
            //movement += new Vector3(1, 0, 0);  右 (X軸方向)
            //movement += new Vector3(1, 0, 0);  右 (X軸方向)

            //右向きに歩くアニメ―ション
            standingState(false);
            walkState(true);
            transform.localScale = new Vector3(1, 1, 1);
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("Jump", 0.6f);//ジャンプの溜めの時間分呼び出しを遅らせる
            isJumping = true;
            Invoke("isJump", 1.9f);
        }

        else if (isJumping)//ジャンプのアニメ―ション
        {
            standingState(false);
            jumpState(true);
        }

        else//何もせず地上にいる状態はstandingアニメーションを動かす。
        {
            standingState(true);
            walkState(false);
            jumpState(false);
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

    void standingState(bool nowState)//立ち状態
    {
        standing.SetActive(nowState);
    }
    void walkState(bool nowState)//歩き状態
    {
        walk.SetActive(nowState);
    }
    void jumpState(bool nowState)//ジャンプ状態
    {
        jump.SetActive(nowState);
    }

    void isJump()
    {
        isJumping = false;
    }
}

