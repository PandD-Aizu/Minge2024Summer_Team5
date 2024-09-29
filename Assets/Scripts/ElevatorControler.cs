using System.Collections;
using UnityEngine;

public class ElevatorControler : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 start;
    public float FloorPosition = 3; // 上昇位置
    public float movespeed = 0.01f;

    public void Start()
    {
        start = transform.position; // 初期位置を保存
        
    }

    // 指定されたy座標の高さに移動
    public void MoveToPosition(float targetPosition)
    {
        StartCoroutine(FloorMove(targetPosition));
    }

    IEnumerator FloorMove(float targetPosition)
    {
        pos = transform.position;

        // 上昇または下降の方向を判定し、目的地に向かって移動
        while (Mathf.Abs(pos.y - targetPosition) > 0.01f) // 目的地との誤差が小さくなったら終了
        {
            pos = transform.position;

            // 移動方向を決定
            float direction = targetPosition > pos.y ? 1 : -1;

            // 指定の速度で移動
            transform.Translate(0, movespeed * direction, 0);

            // フレーム待機
            yield return new WaitForSeconds(0.01f);
        }

        // 目的地にぴったり位置合わせする
        transform.position = new Vector3(transform.position.x, targetPosition, transform.position.z);
    }

    // 上に移動するためのメソッド
    public void MoveUp()
    {
        MoveToPosition(FloorPosition);
    }

    // 下に移動するためのメソッド
    public void MoveDown()
    {
        MoveToPosition(start.y);
    }
}
