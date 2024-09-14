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
        // 入力の取得
        float moveHorizontal = Input.GetAxis("Horizontal");  // 左右の入力 (A, D)

        if (Input.GetKey(KeyCode.A))
        {
            
            //左向きに歩くアニメーション
            if (!isJumping)
            {
                standingState(false);
                walkState(true);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
           
            //右向きに歩くアニメ―ション
            if (!isJumping)
            {
                standingState(false);
                walkState(true);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("Jump", 0.6f);//ジャンプの溜めの時間分呼び出しを遅らせる
            isJumping = true;
            Invoke("isJump", 2.1f);
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
        jumpState(false);
        standingState(true);
    }
}

