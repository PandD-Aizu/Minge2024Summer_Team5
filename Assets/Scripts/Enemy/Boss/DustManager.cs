using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    public GameObject dustPrefab;          // 埃のPrefab
    public int dustAmount = 100;           // 生成する埃の数

    private List<GameObject> dustObjects = new List<GameObject>();

    void Start()
    {
        ScatterDust();  // ゲーム開始時に埃を散らばらせる
    }

    // 埃を画面全体にランダムに散らばらせる処理
    public void ScatterDust()
    {
        // カメラのビューポートの左下(0,0)と右上(1,1)をワールド座標に変換
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // ワールド座標のXとYの範囲を取得
        float minX = bottomLeft.x;
        float maxX = topRight.x;
        float minY = bottomLeft.y;
        float maxY = topRight.y;

        for (int i = 0; i < dustAmount; i++)
        {
            // ランダムな位置に埃を生成（カメラの視界内）
            Vector3 randomPosition = new Vector3(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY),
                -1f
            );

            // 埃のPrefabを生成し、リストに追加
            GameObject dust = Instantiate(dustPrefab, randomPosition, Quaternion.identity);
            dustObjects.Add(dust);
        }
    }
}