using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectScroll : MonoBehaviour
{
    public float scrollSpeed = 5f; // スクロールの速度
    public float minX = -10f;   // カメラのX座標の最小値
    public float maxX = 10f;    // カメラのX座標の最大値

    void Update()
    {
        float camHorizontal= Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position + new Vector3(camHorizontal * scrollSpeed * Time.deltaTime, 0, 0);

        
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);// カメラ範囲

        transform.position = newPosition;
    }
}
