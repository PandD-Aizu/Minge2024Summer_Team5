using UnityEngine;
using UnityEngine.SceneManagement;

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

    // 現在のステージをクリアし、次のステージをアンロックする
    public void ClearCurrentStageAndUnlockNext()
    {
        // 現在のシーンの名前を取得
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 現在のシーンの名前から数字部分を抽出
        int currentStageNumber;
        if (int.TryParse(System.Text.RegularExpressions.Regex.Match(currentSceneName, @"\d+").Value, out currentStageNumber))
        {
            // 次のステージをアンロック
            UnlockStage(currentStageNumber + 1);

            // 次のステージに移動する（シーン名は「Stage」＋次のステージ番号と仮定）
            string nextSceneName = "Stage" + (currentStageNumber + 1);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("現在のシーンの名前からステージ番号を抽出できませんでした: " + currentSceneName);
        }
    }
}
