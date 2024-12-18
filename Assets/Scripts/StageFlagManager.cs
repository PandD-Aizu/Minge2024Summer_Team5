using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageFlagManager : MonoBehaviour
{
    // シングルトンインスタンス
    private static StageFlagManager instance;

    // BGM関連
    private AudioSource audioSource1;  // 一つ目のBGM用
    private AudioSource audioSource2;  // 二つ目のBGM用
    private AudioSource audioSource3;  // 三つ目のBGM用
    private AudioSource audioSource4;  // 四つ目のBGM用
    private AudioSource audioSource5;  // 五つ目のBGM用
    private AudioSource audioSource6;  // 六つ目のBGM用
    private AudioSource audioSource7;  // 七つ目のBGM用
    private AudioSource audioSource8;  // 八つ目のBGM用
    private int currentBGMStage = 0;   // 現在のBGMの段階
    public float volume = 0.6f;        // デフォルトの音量
    public float fadeDuration = 2f;    // フェードイン・フェードアウトの時間

    // AudioClipを設定
    public AudioClip bgmClip1;
    public AudioClip bgmClip2;
    public AudioClip bgmClip3;
    public AudioClip bgmClip4;
    public AudioClip bgmClip5;
    public AudioClip bgmClip6;
    public AudioClip bgmClip7;
    public AudioClip bgmClip8;

    // クリア済みステージの記録
    private HashSet<int> clearedStages = new HashSet<int>();

    private void Awake()
    {
        // インスタンスが存在しない場合は設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーンを切り替えてもこのオブジェクトを破棄しない
            InitializeStageFlags();
            InitializeBGM();
        }
        else
        {
            // インスタンスが既に存在する場合はこのオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioSource[] bgmStages = { audioSource1, audioSource2, audioSource3, audioSource4, audioSource5, audioSource6, audioSource7, audioSource8 };
        bgmStages[0].volume = 0;
    }

    private void Update()
    {
        //Debug用
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceBGMStage();
        }
        */
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
        ResetBGM();  // BGMもリセット
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
            if (currentStageNumber == 3 || currentStageNumber == 4 || currentStageNumber == 5 || currentStageNumber == 8 || currentStageNumber == 11 || currentStageNumber == 12 || currentStageNumber == 15 && !clearedStages.Contains(currentStageNumber)) { 
                AdvanceBGMStage();
                clearedStages.Add(currentStageNumber);  // クリア済みステージに追加
            }

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

    // BGMの初期設定
    public void InitializeBGM()
    {
        // 各AudioSourceを追加
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource3 = gameObject.AddComponent<AudioSource>();
        audioSource4 = gameObject.AddComponent<AudioSource>();
        audioSource5 = gameObject.AddComponent<AudioSource>();
        audioSource6 = gameObject.AddComponent<AudioSource>();
        audioSource7 = gameObject.AddComponent<AudioSource>();
        audioSource8 = gameObject.AddComponent<AudioSource>();

        audioSource1.clip = bgmClip1;
        audioSource2.clip = bgmClip2;
        audioSource3.clip = bgmClip3;
        audioSource4.clip = bgmClip4;
        audioSource5.clip = bgmClip5;
        audioSource6.clip = bgmClip6;
        audioSource7.clip = bgmClip7;
        audioSource8.clip = bgmClip8;


        // 全てのBGMの音量を0に設定し再生
        AudioSource[] bgmStages = { audioSource1, audioSource2, audioSource3, audioSource4, audioSource5, audioSource6, audioSource7, audioSource8 };
        for (int i = 0; i < bgmStages.Length; i++)
        {
            bgmStages[i].volume = 0f;
            bgmStages[i].Play();
        }

        // 最初のBGMの音量を設定
        bgmStages[0].volume = volume;
    }

    // BGMを次の段階に進めるメソッド
    public void AdvanceBGMStage()
    {
        AudioSource[] bgmStages = { audioSource1, audioSource2, audioSource3, audioSource4, audioSource5, audioSource6, audioSource7, audioSource8 };

        if (currentBGMStage < bgmStages.Length - 1)
        {
            // 現在のBGMをフェードアウトし、次のBGMをフェードイン
            StartCoroutine(FadeBetweenBGM(bgmStages[currentBGMStage], bgmStages[currentBGMStage + 1]));

            // BGM段階を進める
            currentBGMStage++;
        }
    }

    // BGMをリセットする
    private void ResetBGM()
    {
        AudioSource[] bgmStages = { audioSource1, audioSource2, audioSource3, audioSource4, audioSource5, audioSource6, audioSource7, audioSource8 };

        if (bgmStages.Length > 0)
        {
            // 全てのBGMを音量0に設定
            for (int i = 0; i < bgmStages.Length; i++)
            {
                bgmStages[i].volume = 0f;
            }

            // 最初のBGMを有効化
            currentBGMStage = 0;
            bgmStages[0].volume = volume;
        }
    }

    // BGMのフェードイン・フェードアウトを同時に行う
    IEnumerator FadeBetweenBGM(AudioSource fadeOutSource, AudioSource fadeInSource)
    {
        float elapsedTime = 0f;

        // 現在のBGMをフェードアウトし、次のBGMをフェードインする
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            fadeOutSource.volume = Mathf.Lerp(volume, 0f, t);  // フェードアウト
            fadeInSource.volume = Mathf.Lerp(0f, volume, t);   // フェードイン

            yield return null;
        }

        // フェード完了後、フェードアウトしたBGMの音量は0に設定
        fadeOutSource.volume = 0f;
        fadeInSource.volume = volume;
    }
}
