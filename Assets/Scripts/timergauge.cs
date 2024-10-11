using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class timergauge : MonoBehaviour
{
    float limit;  // 制限時間
    float limit2;
    float now; // 経過時間
    public Slider timerGauge;   //残り時間ゲージ
    Piano_ChangeSpeedAndJumpPower piano;

    // Start is called before the first frame update
    void Start()
    {

        timerGauge = GetComponent <Slider>();
        piano = GameObject.FindObjectOfType<Piano_ChangeSpeedAndJumpPower>();
        limit = piano.countdown_jumppower;
        limit2 = piano.countdown_speed;
        timerGauge.value = 1f;  //制限時間ゲージ   
        //Debug.Log("value :" + timerGauge.value);

    }

    public void Timer () 
    {
        Debug.Log("aaaaaaaa");
        // 時間制御
        now += Time.deltaTime; // タイマー
        Debug.Log(now);
        float t = now / limit; // スライダーの値ー正規化
        Debug.Log("value :" + timerGauge.value);
        timerGauge.value = Mathf.Lerp(1f, 0f, t);
        float timeLimit = limit - now; // 残り時間
        timeLimit = Mathf.Max(timeLimit, 0f);
    }
}
