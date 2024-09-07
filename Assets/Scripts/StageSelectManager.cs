using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    public Button[] stageButtons; // 各ステージのボタン
    private StageFlagManager stageFlagManager;

    void Start()
    {
        stageFlagManager = FindObjectOfType<StageFlagManager>();
        
        //デバッグ用
        //進行度リセットを行うメソッド
        //stageFlagManager.ResetProgress();
        
        // 各ステージのボタンの有効/無効を設定
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1; // ステージ番号（1から始まる）
            stageButtons[i].interactable = stageFlagManager.IsStageUnlocked(stageNumber);

            // ボタンが押された時の処理を追加
            int capturedStageNumber = stageNumber; // ローカル変数にキャプチャすることで、クロージャ内での使用を安定させる
            stageButtons[i].onClick.AddListener(() => OnStageSelected(capturedStageNumber));
        }
    }



    // ステージ選択時の処理
    public void OnStageSelected(int stageNumber)
    {
        if (stageFlagManager.IsStageUnlocked(stageNumber))
        {
            string sceneName = "Stage" + stageNumber;
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Stage " + stageNumber + " is locked.");
        }
    }


}
