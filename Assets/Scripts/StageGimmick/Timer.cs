using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Transform clockHand;  // 時計の針のTransform
    public Image timerImage; // タイマー用のImage
    public float totalTime = 10f; // カウントダウン時間
    private float remainingTime;

    public GameObject clearArea;
    public GameObject AreaRock;

    void Start()
    {
        remainingTime = totalTime;

        clearArea = GameObject.Find("clearArea");
        if (clearArea == null)
        {
            Debug.LogWarning("clearArea not Found");
        }
        else { 
            clearArea.SetActive(false);
        }

        AreaRock = GameObject.Find("areaRock");
        if (AreaRock == null)
        {
            Debug.LogWarning("areaRock not Found");
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            // 時間を減らす
            remainingTime -= Time.deltaTime;

            // Fill Amountを更新（1から0へ変化）
            timerImage.fillAmount = remainingTime / totalTime;

            // 針の回転を更新（時計回りに回す）
            float rotationAngle = (remainingTime / totalTime) * 360f;
            clockHand.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
        else
        {
            // タイマーが0以下になったら何か処理を行う
            remainingTime = 0;
            // タイムアップの処理をここに追加

            if (clearArea) { 
                clearArea.SetActive(true);
                AreaRock.SetActive(false);
            }
        }
    }
}
