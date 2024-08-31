using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clearArea : MonoBehaviour
{
    StageFlagManager stageFlagManager;
    bool is_Branced;
    // Start is called before the first frame update
    void Start()
    {
        //分岐のあるなし
        is_Branced = false;
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
        }
    }

    // ステージがクリアされたときに呼び出されるメソッド
    //このメソッドはSceneの名前から次のステージを取ってるのでもし分岐させる場合は自分で引数を設定したUnlookStage()を呼び出す必要があるので注意。
    public void OnStageCleared()
    {
        // StageFlagManagerのインスタンスを取得して、次のステージをアンロック
        StageFlagManager stageFlagManager = FindObjectOfType<StageFlagManager>();

        if (stageFlagManager != null)
        {
            stageFlagManager.ClearCurrentStageAndUnlockNext();
        }
        else
        {
            Debug.LogWarning("StageFlagManagerが見つかりませんでした。");
        }
    }
}
