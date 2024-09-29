using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bass_MousePointer : MonoBehaviour
{
    public float rotationSpeed = 4f;  // 回転速度
    private GameObject rotatingObject = null;  // 回転中のオブジェクト
    private Quaternion targetRotation;  // ターゲットの回転角度
    private bool isRotating = false;  // 回転中かどうかを管理するフラグ
    public float stopThreshold = 0.5f; // 回転完了とみなす角度の閾値

    private void Update()
    {
        // オブジェクトを回転させる処理
        if (isRotating && rotatingObject != null)
        {
            // 現在の回転をターゲット回転へスムーズに変化させる
            rotatingObject.transform.rotation = Quaternion.Slerp(
                rotatingObject.transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
            );

            // 回転が完了とみなす角度差のチェック
            if (Quaternion.Angle(rotatingObject.transform.rotation, targetRotation) < stopThreshold)
            {
                // 最終的に目標角度に到達させる
                rotatingObject.transform.rotation = targetRotation;
                rotatingObject = null; // 回転完了後にオブジェクトの参照をクリア
                isRotating = false;    // 回転が完了したらフラグをオフにする
            }
        }

        // 回転中でない場合にクリックを判定
        if (!isRotating && Input.GetMouseButtonDown(0)) // 左クリックを判定
        {
            // メインカメラ上のマウスカーソルのある位置からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // Rayが当たったオブジェクト名をログに表示
                Debug.Log(hit.collider.gameObject.name);

                // "LaserReflector"タグを持つオブジェクトに当たった場合
                if (hit.collider.gameObject.tag == "LaserReflector")
                {
                    // 回転対象のオブジェクトとターゲットの回転角度を設定
                    rotatingObject = hit.collider.gameObject;

                    // 回転対象のオブジェクトの角度がずれないように45度単位で再調整
                    rotatingObject.transform.rotation = Quaternion.Euler(
                        Mathf.Round(rotatingObject.transform.rotation.eulerAngles.x / 45) * 45,
                        Mathf.Round(rotatingObject.transform.rotation.eulerAngles.y / 45) * 45,
                        Mathf.Round(rotatingObject.transform.rotation.eulerAngles.z / 45) * 45
                    );

                    // ターゲットの回転角度を設定（Z軸に90度回転）
                    targetRotation = rotatingObject.transform.rotation * Quaternion.Euler(0, 0, 90);
                    isRotating = true; // 回転中フラグをオンにする
                }
            }
        }
    }
}
