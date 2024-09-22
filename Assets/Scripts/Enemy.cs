using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float moveDistance = 5f;  // 右に進む距離
    public float moveSpeed = 2f;     // 移動速度
    public float waitTime = 1f;      // 次の移動までの待機時間

    private Vector3 startPoint;      // 初期位置
    public bool movingRight = true; // 現在の移動方向（true = 右, false = 左）

    void Start()
    {
        // 敵の初期位置を記録
        startPoint = transform.position;

        // コルーチンを開始
        StartCoroutine(Patrol());
    }

    // 敵が左右に移動するコルーチン
    IEnumerator Patrol()
    {
        while (true)
        {
            // 目的地を計算
            Vector3 targetPoint = movingRight ? startPoint + Vector3.right * moveDistance : startPoint - Vector3.right * moveDistance;

            // 目的地まで移動する
            while (Vector3.Distance(transform.position, targetPoint) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
                yield return null; // フレームごとに処理を待機
            }

            // 移動完了後、少し待機
            yield return new WaitForSeconds(waitTime);

            // 移動方向を反転
            movingRight = !movingRight;

            //transform.eulerAngles = new Vector3(0, 180, 0);//方向を180度変える。
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);

        }
    }
}