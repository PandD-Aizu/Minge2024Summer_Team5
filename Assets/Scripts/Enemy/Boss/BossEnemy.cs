using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float attackInterval = 5f; // 攻撃間隔

    // 各攻撃スクリプトへの参照
    //private EnemySummoner enemySummoner;
    [SerializeField]
    private DustManager dustManager;
    [SerializeField]
    private ChargeAttacker chargeAttacker;
    [SerializeField]
    private enemy360 enemy360;

    void Start()
    {
        // 各攻撃スクリプトを取得
        //enemySummoner = GetComponent<EnemySummoner>();
        dustManager = GetComponent<DustManager>();
        chargeAttacker = GetComponent<ChargeAttacker>();
        enemy360 = GetComponent<enemy360>();

        
        // ボスの攻撃を定期的に実行
        StartCoroutine(AttackRoutine());
    }

    // ランダムに攻撃を選択して実行するコルーチン
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // 攻撃間隔の待機
            yield return new WaitForSeconds(attackInterval);

            // ランダムに攻撃を選択
            int attackIndex = Random.Range(0, 4); // 0〜3の範囲でランダムな攻撃を選ぶ
            Debug.Log(attackIndex);
            switch (attackIndex)
            {
                case 0:
                    //enemySummoner.SummonEnemies(); // 雑魚敵召喚
                    chargeAttacker.StartChargeMovement();
                    break;
                case 1:
                    dustManager.ScatterDust(); // 埃をかぶせる
                    break;
                case 2:
                    chargeAttacker.StartChargeMovement(); // 突進攻撃
                    break;
                case 3:
                    StartCoroutine(enemy360.Shottime()); // 弾幕攻撃
                    break;
            }
        }
    }
}
