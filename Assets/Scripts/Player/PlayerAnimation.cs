using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //動作によって表示するオブジェクトを切り替える。モーションの数だけオブジェクトを用意する。
    public GameObject walk;
    public GameObject standing;
    public GameObject jump;

    public bool isJumping = false;//ジャンプ中であるかどうか(isGroundとは違う扱い、ジャンプ中は移動アニメーションを描画しないようにするため)

    void Start()
    {
        isJumping = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            standingState(false);
            walkState(false);
            Invoke("isNotJump", 1.1f);//着地までにかかる時間分isJumpingをtrueにする
        }

        else if (Input.GetKey(KeyCode.A))
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

        if (isJumping)//ジャンプのアニメ―ション
        {
            standingState(false);
            jumpState(true);
        }
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

    void isNotJump()
    {
        isJumping = false;
        jumpState(false);
        standingState(true);
    }
}

