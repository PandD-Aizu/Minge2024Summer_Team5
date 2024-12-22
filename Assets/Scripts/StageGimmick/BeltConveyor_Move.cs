using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor_Move : MonoBehaviour
{
    public float conveyorSpeed = 5.0f;
    public int conveyorSwitch = 0;

    private Vector3 conveyorDirection;

    void Update()
    {
        // コンベアの移動方向を決定
        if (conveyorSwitch == 0)
        {
            conveyorDirection = Vector3.right; // 右方向
        }
        else if (conveyorSwitch == 1)
        {
            conveyorDirection = Vector3.left; // 左方向
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Playerタグを持つオブジェクトのみ処理
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player on conveyor belt");

            // Rigidbodyを取得
            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // ベルトの方向に一定の力を加える
                playerRb.velocity = conveyorDirection * conveyorSpeed;
            }
        }
    }
}
