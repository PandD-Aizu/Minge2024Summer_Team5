using UnityEngine;
using UnityEngine.UI;

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
        }
    }

    //動作確認用にボタンの有効/無効をUpdateで呼び出している。本来はStatに書いてある分で事足りる。
    private void Update()
    {
        // 各ステージのボタンの有効/無効を設定
        for (int i = 0; i < stageButtons.Length; i++)
        {
            int stageNumber = i + 1; // ステージ番号（1から始まる）
            stageButtons[i].interactable = stageFlagManager.IsStageUnlocked(stageNumber);
        }
    }

    // ステージ選択時の処理を追加（オプション）
    public void OnStageSelected(int stageNumber)
    {
        if (stageFlagManager.IsStageUnlocked(stageNumber))
        {
            // ステージをロードするなどの処理
            Debug.Log("Loading Stage " + stageNumber);
        }
        else
        {
            Debug.Log("Stage " + stageNumber + " is locked.");
        }
    }
}
