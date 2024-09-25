using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceSystem : MonoBehaviour
{
    public Transform wall;           // 壁のTransform
    public Transform pulleyTop;      // ロープが通る重り側の滑車
    public Transform pulleyTopWall;  // ロープが通る壁側の滑車
    public float maxWallHeight = 5f; // 壁が上がる最大の高さ
    public float maxTriggerDepth = -5f; // トリガーが下がる最大の深さ
    public float moveSpeed = 2f;     // 移動速度
    private float targetWallHeight;  // 壁の目標高さ
    private float targetTriggerHeight; // トリガーの目標深さ
    private float currentWeight = 0f;  // 現在の重りの総重量
    public float weightFactor = 1f;   // 重さに応じて上下する度合い

    private Vector3 initialWallPosition;  // 壁の初期位置
    private Vector3 initialTriggerPosition; // トリガーの初期位置

    private LineRenderer lineRenderer;    // ロープを描画するためのLineRenderer

    // Startメソッドで壁とトリガーの初期位置を記録
    private void Start()
    {
        initialWallPosition = wall.position;
        initialTriggerPosition = transform.position; // トリガー自身の位置を取得
        targetWallHeight = initialWallPosition.y;
        targetTriggerHeight = initialTriggerPosition.y;

        // LineRendererを追加してロープを描画
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;  // ロープの始点の太さ
        lineRenderer.endWidth = 0.05f;    // ロープの終点の太さ
        lineRenderer.positionCount = 4;   // ロープの頂点数（重り→重りの滑車→壁の滑車→壁）

        // シンプルな白色のマテリアルを作成
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // シンプルなマテリアル
        lineRenderer.startColor = new Color(0.7f, 0.7f, 0.7f);  // 灰色に設定
        lineRenderer.endColor = new Color(0.7f, 0.7f, 0.7f);    // 終点も灰色に設定
    }

    // トリガーに入った重りの処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragable")) // 重りオブジェクトか確認
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                currentWeight += rb.mass; // 重量を加算
                UpdateTargetHeights();
            }
        }
    }

    // トリガーから出た重りの処理
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Dragable")) // 重りオブジェクトか確認
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                currentWeight -= rb.mass; // 重量を減算
                UpdateTargetHeights();
            }
        }
    }

    // 壁とトリガーの目標位置を更新
    private void UpdateTargetHeights()
    {
        // 重さに応じてトリガーが下がり、壁が上がる高さを計算
        float triggerHeightChange = Mathf.Clamp(-currentWeight * weightFactor, maxTriggerDepth, 0f);
        float wallHeightChange = Mathf.Clamp(currentWeight * weightFactor, 0f, maxWallHeight);

        targetTriggerHeight = initialTriggerPosition.y + triggerHeightChange; // トリガーの目標位置
        targetWallHeight = initialWallPosition.y + wallHeightChange;         // 壁の目標位置
    }

    private void Update()
    {
        // トリガーの位置を目標位置にスムーズに移動
        Vector3 triggerPosition = transform.position;
        triggerPosition.y = Mathf.Lerp(triggerPosition.y, targetTriggerHeight, Time.deltaTime * moveSpeed);
        transform.position = triggerPosition;

        // 壁の位置を目標位置にスムーズに移動
        Vector3 wallPosition = wall.position;
        wallPosition.y = Mathf.Lerp(wallPosition.y, targetWallHeight, Time.deltaTime * moveSpeed);
        wall.position = wallPosition;

        // ロープ（LineRenderer）の位置を更新
        UpdateRope();
    }

    // ロープの位置を更新
    private void UpdateRope()
    {
        // ロープの4つのポイントを設定: 重り（トリガー）→ 重り側の滑車 → 壁側の滑車 → 壁
        lineRenderer.SetPosition(0, transform.position);  // 重りの位置
        lineRenderer.SetPosition(1, pulleyTop.position);  // 重り側の滑車の位置
        lineRenderer.SetPosition(2, pulleyTopWall.position);  // 壁側の滑車の位置
        lineRenderer.SetPosition(3, wall.position);       // 壁の位置
    }
}

