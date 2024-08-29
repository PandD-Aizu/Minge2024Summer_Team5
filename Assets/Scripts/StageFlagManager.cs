using UnityEngine;

public class StageFlagManager : MonoBehaviour
{
    // シングルトンインスタンス
    private static StageFlagManager instance;

    private void Awake()
    {
        // インスタンスが存在しない場合は設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーンを切り替えてもこのオブジェクトを破棄しない
            InitializeStageFlags();
        }
        else
        {
            // インスタンスが既に存在する場合はこのオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    // 1ステージ目の解禁を初期化
    private void InitializeStageFlags()
    {
        if (!PlayerPrefs.HasKey("Stage1"))
        {
            UnlockStage(1);
        }
    }

    // ステージを解禁する
    public void UnlockStage(int stageNumber)
    {
        PlayerPrefs.SetInt("Stage" + stageNumber, 1);
        PlayerPrefs.Save();
    }

    // ステージが解禁されているかを確認する
    public bool IsStageUnlocked(int stageNumber)
    {
        return PlayerPrefs.GetInt("Stage" + stageNumber, 0) == 1;
    }

    // 全ステージの進行状況をリセットする
    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        InitializeStageFlags();  // 初期ステージ解禁を再設定
    }

    //以下動作確認の為の仮置きのメソッド、本来はそれぞれのSceneの名前等から数字を取ってきて UnlockStageの変数を自動で決定出来るようにしたい。
    public void ClearedStage1()
    {
        UnlockStage(2);
    }

    public void ClearedStage2()
    {
        UnlockStage(3);
    }
    public void ClearedStage3_1()
    {
        UnlockStage(4);
    }
    public void ClearedStage3_2()
    {
        UnlockStage(5);
    }
}
