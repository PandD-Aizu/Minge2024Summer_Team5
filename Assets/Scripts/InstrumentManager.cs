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

    // 楽器の利用可能フラグ
    public bool isDrumAvailable = false;
    public bool isBassAvailable = false;
    public bool isPianoAvailable = false;

    // 現在選択中の楽器インデックス (0: Drum, 1: Bass, 2: Piano)
    private int currentInstrumentIndex = 0;
    private List<GameObject> instruments;
    private List<bool> instrumentAvailability;

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

            // 楽器の利用可能フラグをリストに追加
            instrumentAvailability = new List<bool> { isDrumAvailable, isBassAvailable, isPianoAvailable };

            // 初期状態ではすべて非アクティブにする
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(false);
            }

            // ドラムをデフォルトでアクティブにする（有効であれば）
            if (isDrumAvailable)
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
            SwitchToNextInstrument(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToNextInstrument(-1);
        }
    }

    // 楽器を切り替える処理
    private void SwitchToNextInstrument(int direction)
    {
        int originalIndex = currentInstrumentIndex;

        // 次の楽器に移動
        do
        {
            currentInstrumentIndex = (currentInstrumentIndex + direction + instruments.Count) % instruments.Count;
        } while (!instrumentAvailability[currentInstrumentIndex] && currentInstrumentIndex != originalIndex);

        // 有効な楽器が見つかれば切り替え
        if (instrumentAvailability[currentInstrumentIndex])
        {
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(false);
            }
            instruments[currentInstrumentIndex].SetActive(true);
        }
    }

    // 楽器の利用可能フラグを更新するメソッド
    public void UpdateInstrumentAvailability(bool drumAvailable, bool bassAvailable, bool pianoAvailable)
    {
        isDrumAvailable = drumAvailable;
        isBassAvailable = bassAvailable;
        isPianoAvailable = pianoAvailable;

        // フラグをリストに反映
        instrumentAvailability[0] = isDrumAvailable;
        instrumentAvailability[1] = isBassAvailable;
        instrumentAvailability[2] = isPianoAvailable;
    }
}
