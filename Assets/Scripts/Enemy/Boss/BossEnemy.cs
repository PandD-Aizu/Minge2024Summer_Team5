using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public float attackInterval = 5f;  // 攻撃間隔
    public float weakStateDuration = 5f;  // 弱点露呈状態の時間
    public GameObject[] bossPhases;    // 各フェーズで表示するボスのGameObjectを格納
    public int currentPhase = 0;       // 現在のフェーズ
    public bool isWeakState = false;   // 弱点露呈状態かどうか
    public int maxPhases = 8;          // フェーズの最大数
    public bool isAttacking = true;   // 攻撃中かどうか

    private Coroutine attackRoutineCoroutine; // 攻撃ルーチンのコルーチン
    private Coroutine weakStateCoroutine;     // 弱点露呈状態のコルーチン

    // 各攻撃スクリプトへの参照
    [SerializeField]
    private DustManager dustManager;
    [SerializeField]
    private ChargeAttacker chargeAttacker;
    [SerializeField]
    private enemy360 enemy360;

    void Start()
    {
        // 各攻撃スクリプトを取得
        dustManager = GetComponent<DustManager>();
        chargeAttacker = GetComponent<ChargeAttacker>();
        enemy360 = GetComponent<enemy360>();

        // ボスの見た目を初期化
        UpdateBossAppearance();

        // ボスの攻撃を定期的に実行
        attackRoutineCoroutine = StartCoroutine(AttackRoutine());
    }

    // ランダムに攻撃を選択して実行するコルーチン
    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // 攻撃間隔の待機
            yield return new WaitForSeconds(attackInterval);

            if (!isAttacking) yield break; // 攻撃が停止中ならコルーチンを終了

            // ランダムに攻撃を選択
            int attackIndex = Random.Range(0, 4); // 0〜3の範囲でランダムな攻撃を選ぶ
            Debug.Log(attackIndex);
            switch (attackIndex)
            {
                case 0:
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

    // ボスがダメージを受けたときに呼び出すメソッド
    public void TakeDamage()
    {
        if (isWeakState && weakStateCoroutine != null)
        {
            // 弱点露呈状態で攻撃を受けたので、コルーチンを停止して進行
            StopCoroutine(weakStateCoroutine);
            weakStateCoroutine = null;
        }

        if (currentPhase < maxPhases - 1)
        {
            currentPhase++;
            UpdateBossAppearance();

            // 偶数フェーズでは弱点露呈状態になる
            if (currentPhase % 2 == 1)
            {
                EnterWeakState();
            }
            else
            {
                ExitWeakState();
            }
        }
        else
        {
            BossDestroyed();
        }
    }

    // ボスの見た目をフェーズに応じて切り替える
    private void UpdateBossAppearance()
    {
        for (int i = 0; i < bossPhases.Length; i++)
        {
            bossPhases[i].SetActive(i == currentPhase);
        }
    }

    // 弱点露呈状態に入る
    private void EnterWeakState()
    {
        Debug.Log("EntryWeakState!");
        isWeakState = true;
        StopAttacks();
        // 5秒間攻撃を受けなかった場合にフェーズを戻すコルーチンを開始
        weakStateCoroutine = StartCoroutine(WeakStateDurationRoutine());

        // 弱点露呈状態のアニメーションやエフェクトなどを追加する場合はここに記載
    }

    // 弱点露呈状態を抜ける
    private void ExitWeakState()
    {
        Debug.Log("ExitWeakState!");
        isWeakState = false;
        ResumeAttacks();
    }

    // 弱点露呈状態で一定時間攻撃を受けなかった場合にフェーズを戻すコルーチン
    IEnumerator WeakStateDurationRoutine()
    {
        yield return new WaitForSeconds(weakStateDuration); // 5秒待機

        if (isWeakState)
        {
            // 弱点露呈状態が続いている場合はひとつ前のフェーズに戻す
            currentPhase--;
            UpdateBossAppearance();
            ExitWeakState(); // 弱点露呈状態を終了
        }
    }

    // 攻撃を停止する
    private void StopAttacks()
    {
        isAttacking = false;
        if (attackRoutineCoroutine != null)
        {
            StopCoroutine(attackRoutineCoroutine); // コルーチンを停止
            attackRoutineCoroutine = null;
        }
    }

    // 攻撃を再開する
    private void ResumeAttacks()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackRoutineCoroutine = StartCoroutine(AttackRoutine()); // コルーチンを再開
        }
    }

    // ボスが最終フェーズに到達して破壊される
    private void BossDestroyed()
    {
        // ボスの破壊処理やエフェクトを実行
        Debug.Log("Boss Destroyed!");
    }
}

