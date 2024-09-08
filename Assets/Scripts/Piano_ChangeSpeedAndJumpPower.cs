using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano_ChangeSpeedAndJumpPower : MonoBehaviour{
    public Player Player;
    public int pianochange_speed = 0;/*切り替えスイッチ*/
    public int pianochange_jumppower = 0;
    float speed_p = 15f;    /*変換後の速度*/
    int jumppower_p = 350;  /*変換後のジャンプ力*/

    private void Start()
    {
        Player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (pianochange_speed == 1)
        {
            Player.speed = speed_p;
        }
        else
        {
            Player.speed = 10f;/*戻す*/
        }

        if (pianochange_jumppower == 1)
        {
            Player.jumppower = jumppower_p;
        }
        else
        {
            Player.jumppower = 250;/*戻す*/
        }
    }
    

}

