using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrumentManager : MonoBehaviour
{
    // シングルトンインスタンス
    private static InstrumentManager instance;
    public GameObject DrumManager;
    public GameObject BassManager;
    public GameObject PianoManager;

    // 現在選択中の楽器インデックス (0: Drum, 1: Bass, 2: Piano)
    private int currentInstrumentIndex = 0;
    private List<GameObject> instruments;

    private void Awake()
    {
        // インスタンスが存在しない場合は設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーンを切り替えてもこのオブジェクトを破棄しない
        }
        else
        {
            // インスタンスが既に存在する場合はこのオブジェクトを破棄
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        
    }

    // シーンがロードされたときに実行されるメソッド
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("シーンがロードされました: " + scene.name);
        // 楽器オブジェクトを取得
        if (scene.name != "Title" && scene.name != "StageSelectScene")
        {
            DrumManager = GameObject.Find("DrumManager");
            BassManager = GameObject.Find("BassManager");
            PianoManager = GameObject.Find("PianoManager");

            // リストに楽器を追加
            instruments = new List<GameObject> { DrumManager, BassManager, PianoManager };

            // 初期状態ではすべて非アクティブにする
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(false);
            }

            // ドラムをデフォルトでアクティブにする
            if (DrumManager != null)
            {
                DrumManager.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 右キーで次の楽器、左キーで前の楽器に切り替え
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentInstrumentIndex = (currentInstrumentIndex + 1) % instruments.Count; // 次の楽器へ
            SwitchInstrument(currentInstrumentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentInstrumentIndex = (currentInstrumentIndex - 1 + instruments.Count) % instruments.Count; // 前の楽器へ
            SwitchInstrument(currentInstrumentIndex);
        }
    }

    // 楽器を切り替える処理
    private void SwitchInstrument(int instrumentIndex)
    {
        if (instruments == null || instrumentIndex < 0 || instrumentIndex >= instruments.Count || instruments[instrumentIndex] == null)
        {
            return;
        }

        // すべての楽器を非アクティブにする
        foreach (GameObject instrument in instruments)
        {
            instrument.SetActive(false);
        }

        // 新しい楽器をアクティブにする
        instruments[instrumentIndex].SetActive(true);
    }
}
