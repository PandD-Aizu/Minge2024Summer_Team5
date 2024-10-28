using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StaffRoll : MonoBehaviour
{
    public GameObject StaffRollImage;  // スタッフロールの画像

    public float slideDistance = 3500f;  // スライド距離
    public float slideSpeed = 10;    // スライド速度

    void Start()
    {
        StaffRollImage = GameObject.Find("StaffRoll");
    }

    void Update()
    {
        if (StaffRollImage.transform.localPosition.y < slideDistance )// StaffRollを上にスライドさせる
        {
            StaffRollImage.transform.Translate(Vector3.up * slideSpeed * Time.deltaTime);
            //Debug.Log(StaffRollImage.transform.localPosition.y);
        }

        // スライド終了後
        else //if (StaffRollImage.transform.localPosition.x >= slideDistance)
        {
            Invoke("MoveToTitle", 3);
        }
    }

    void MoveToTitle()
    {
        SceneManager.LoadScene("Title");  // シーン遷移
    }
}
