using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clearArea : MonoBehaviour
{
    StageFlagManager stageFlagManager;
    bool is_Branced;
    // Start is called before the first frame update
    void Start()
    {
        //分岐のあるなし
        is_Branced = false;
        // StageFlagManagerのインスタンスを取得
        stageFlagManager = FindObjectOfType<StageFlagManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (!is_Branced)
            {
                OnStageCleared();
            }
            else
            {
                // 分岐がある場合は分岐処理
                OnBranchedStageCleared();
            }
        }
    }

    // ステージがクリアされたときに呼び出されるメソッド
    //このメソッドはSceneの名前から次のステージを取ってるのでもし分岐させる場合は自分で引数を設定したUnlookStage()を呼び出す必要があるので注意。
    public void OnStageCleared()
    {
        //次のステージをアンロック

        if (stageFlagManager != null)
        {
            stageFlagManager.ClearCurrentStageAndUnlockNext();
        }
        else
        {
            Debug.LogWarning("StageFlagManagerが見つかりませんでした。");
        }
    }

    void OnBranchedStageCleared() {
        // 現在のシーンの名前を取得
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        //Stage3の分岐
        if (currentSceneName == "Stage3") {
            if (this.name == "clearArea1") {
                stageFlagManager.UnlockStage(4);
                SceneManager.LoadScene("Stage4");
            }
            if (this.name == "clearArea2")
            {
                stageFlagManager.UnlockStage(5);
                SceneManager.LoadScene("Stage5");
            }
        }
    }
}
