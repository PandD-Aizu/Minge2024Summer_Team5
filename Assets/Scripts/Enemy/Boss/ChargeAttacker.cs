using System.Collections;
using UnityEngine;

public class ChargeAttacker : MonoBehaviour
{
    public Transform CheckPointLeftTop;    // 左上のチェックポイント
    public Transform CheckPointLeftBottom; // 左下のチェックポイント
    public Transform CheckPointRightTop;   // 右上のチェックポイント
    public Transform CheckPointRightBottom;// 右下のチェックポイント
    public float moveSpeed = 5f;           // 移動速度
    public float pauseDuration = 1f;       // 各ポイントでの停止時間
    public float positionThreshold = 0.1f; // 中央判定の閾値

    private Transform[] rightSideCheckpoints;   // ボスが右にいるときのチェックポイント配列
    private Transform[] leftSideCheckpoints;    // ボスが左にいるときのチェックポイント配列
    private Transform[] currentCheckpoints;     // 現在のチェックポイント配列
    private int currentCheckpointIndex = 0;     // 現在のチェックポイントのインデックス
    private bool isMoving = false;              // 移動中かどうか
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 右にいるときのチェックポイント順
        rightSideCheckpoints = new Transform[] {
            CheckPointRightTop,
            CheckPointRightBottom,
            CheckPointLeftBottom,
            CheckPointLeftTop
        };

        // 左にいるときのチェックポイント順
        leftSideCheckpoints = new Transform[] {
            CheckPointLeftTop,
            CheckPointLeftBottom,
            CheckPointRightBottom,
            CheckPointRightTop
        };

        // 初期チェックポイントの設定
        SetCheckpointOrder();
    }

    public void StartChargeMovement()
    {
        if (!isMoving)
        {
            // チェックポイントインデックスとフラグのリセット
            SetCheckpointOrder();
            currentCheckpointIndex = 0;
            isMoving = true;

            // 再度コルーチンを開始
            StartCoroutine(MoveToNextCheckpoint());
        }
    }

    void SetCheckpointOrder()
    {
        // ボスが右にいる場合と左にいる場合で順序を設定
        if (transform.position.x > positionThreshold)
        {
            // 右にいる場合
            currentCheckpoints = rightSideCheckpoints;
        }
        else if (transform.position.x < -positionThreshold)
        {
            // 左にいる場合
            currentCheckpoints = leftSideCheckpoints;
        }
    }

    public IEnumerator MoveToNextCheckpoint()
    {
        // 各チェックポイントに移動
        while (currentCheckpointIndex < currentCheckpoints.Length)
        {
            Transform targetCheckpoint = currentCheckpoints[currentCheckpointIndex];

            // ターゲットチェックポイントに向かって移動
            while (Vector3.Distance(transform.position, targetCheckpoint.position) > 0.1f)
            {
                Vector3 direction = (targetCheckpoint.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
                yield return null;
            }

            // 目的地に到達したら停止して次の動作まで少し待機
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(pauseDuration);

            // 次のチェックポイントに進む
            currentCheckpointIndex++;
        }

        // 全チェックポイントを通過後、移動を停止
        isMoving = false;
        rb.velocity = Vector3.zero;
    }
}
