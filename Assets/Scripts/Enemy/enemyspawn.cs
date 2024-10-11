using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyspawn : MonoBehaviour
{
    //敵プレハブ
    public GameObject enemyPrefab;
    //時間間隔の最小値
    public float minTime = 2f;
    //時間間隔の最大値
    public float maxTime = 5f;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    public float moveDistance = 5f;  // 右に進む距離
    public float moveSpeed = 2f;     // 移動速度
    public float waitTime = 1f;      // 次の移動までの待機時間
    public bool movingRight = true;
    public bool isSpowning = false;

    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = GetRandomTime();
        time = minTime;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
            Spawn();
    }

    public void Spawn()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = this.gameObject.transform.position;
            enemy.AddComponent<IgnoreEnemyCollision>();

            Destroy(enemy, 6f);

            // Enemyコンポーネントをプレハブから取得する
            Enemy enemysq = enemy.GetComponent<Enemy>();

            // Enemyコンポーネントが存在するか確認する
            if (enemysq != null)
            {
                enemysq.moveDistance = moveDistance;
                enemysq.moveSpeed = moveSpeed;
                enemysq.waitTime = waitTime;
                enemysq.movingRight = movingRight;
            }
            else
            {
                Debug.LogError("Enemyコンポーネントが見つかりません。enemyPrefabにEnemyコンポーネントがアタッチされているか確認してください。");
            }

            //経過時間を初期化して再度時間計測を始める
            time = 0f;
            //次に発生する時間間隔を決定する
            interval = GetRandomTime();
        }
    }

    //ランダムな時間を生成する関数
    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }
}
