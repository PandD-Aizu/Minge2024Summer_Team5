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

    // チュートリアル用オブジェクト
    public GameObject DrumTutorial;
    public GameObject BassTutorial;
    public GameObject PianoTutorial;

    // 楽器の可用性フラグ
    public bool isDrumAvailable = false;
    public bool isBassAvailable = false;
    public bool isPianoAvailable = false;

    private bool wasDrumAvailable = false;
    private bool wasBassAvailable = false;
    private bool wasPianoAvailable = false;

    // 現在選択されている楽器インデックス (0: Drum, 1: Bass, 2: Piano)
    private int currentInstrumentIndex = 0;
    private List<GameObject> instruments;
    private List<bool> instrumentAvailability;

    // 楽器アイコンが配置された円形のオブジェクト
    public GameObject instrumentWheel;

    public StageFlagManager stageFlagManager;

    public Drumtimer drumtimer;

    //ピアノタイマーのcanvas

    public GameObject dtimer;
    public GameObject pianotimer;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        stageFlagManager = GameObject.FindAnyObjectByType<StageFlagManager>();
        drumtimer = GameObject.FindObjectOfType<Drumtimer>();
        InstrumentInit();

        var sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "switchInstrument") {
            InstrumentInit();
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("シーンロード完了: " + scene.name);
        if (scene.name != "Title" && scene.name != "StageSelectScene")
        {
            InstrumentInit();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextInstrument(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToNextInstrument(-1);
        }

        CheckInstrumentAvailabilityChange();

        /*if (drumtimer. <= 0 && currentInstrumentIndex != 0)
        {
            dtimer.SetActive(false);
        }*/

        if (currentInstrumentIndex == 0)
        {
            dtimer.SetActive(true);
        }
        else if (drumtimer.impact_cooltime == 6.0f && drumtimer.shield_cooltime == 6.0f && currentInstrumentIndex != 0)
        {
            dtimer.SetActive(false);
        }

        if (currentInstrumentIndex == 2)
        {
            pianotimer.SetActive(true);
        }
    }

    private void SwitchToNextInstrument(int direction)
    {
        int originalIndex = currentInstrumentIndex;

        do
        {
            currentInstrumentIndex = (currentInstrumentIndex + direction + instruments.Count) % instruments.Count;
        } while (!instrumentAvailability[currentInstrumentIndex] && currentInstrumentIndex != originalIndex);

        if (instrumentAvailability[currentInstrumentIndex])
        {
            foreach (GameObject instrument in instruments)
            {
                instrument.SetActive(false);
            }
            instruments[currentInstrumentIndex].SetActive(true);

            // 楽器アイコンの回転
            RotateInstrumentWheel(currentInstrumentIndex);
        }
    }

    private void RotateInstrumentWheel(int instrumentIndex)
    {
        // 各楽器に対応するアイコンの回転角度を計算（120度ごとに配置）
        float targetRotation = -120f * instrumentIndex;
        StartCoroutine(SmoothRotateWheel(targetRotation));
    }

    private IEnumerator SmoothRotateWheel(float targetRotation)
    {
        float currentRotation = instrumentWheel.transform.eulerAngles.z;
        //float rotationSpeed = 5f;  // 回転速度をより緩やかにする
        float rotationStep = 0.05f;  // 回転ステップを小さくして滑らかに

        while (Mathf.Abs(Mathf.DeltaAngle(currentRotation, targetRotation)) > 0.1f)
        {
            currentRotation = Mathf.LerpAngle(currentRotation, targetRotation, rotationStep);
            instrumentWheel.transform.eulerAngles = new Vector3(0, 0, currentRotation);
            yield return new WaitForEndOfFrame();  // フレームごとに更新する
        }

        // 最終的な位置をしっかり設定
        instrumentWheel.transform.eulerAngles = new Vector3(0, 0, targetRotation);
    }


    public void UpdateInstrumentAvailability(bool drumAvailable, bool bassAvailable, bool pianoAvailable)
    {
        isDrumAvailable = drumAvailable;
        isBassAvailable = bassAvailable;
        isPianoAvailable = pianoAvailable;

        instrumentAvailability[0] = isDrumAvailable;
        instrumentAvailability[1] = isBassAvailable;
        instrumentAvailability[2] = isPianoAvailable;
    }

    private void CheckInstrumentAvailabilityChange()
    {
        if (DrumTutorial)
        {
            if (isDrumAvailable && !wasDrumAvailable)
            {
                stageFlagManager.InitializeBGM();
                DrumTutorial.SetActive(true);
                Time.timeScale = 0f;
                //Debug.Log("ドラムチュートリアル表示");
            }
        }

        if (BassTutorial)
        {
            if (isBassAvailable && !wasBassAvailable)
            {
                stageFlagManager.AdvanceBGMStage();
                BassTutorial.SetActive(true);
                Time.timeScale = 0f;
                //Debug.Log("ベースチュートリアル表示");
            }
        }

        if (PianoTutorial)
        {
            if (isPianoAvailable && !wasPianoAvailable)
            {
                stageFlagManager.AdvanceBGMStage();
                PianoTutorial.SetActive(true);
                Time.timeScale = 0f;
                //Debug.Log("ピアノチュートリアル表示");
            }
        }


        // 状態の更新
        wasDrumAvailable = isDrumAvailable;
        wasBassAvailable = isBassAvailable;
        wasPianoAvailable = isPianoAvailable;
    }

    //InstrumentManagerの初期化
    public void InstrumentInit() {
        currentInstrumentIndex = 0;

        if (DrumManager == null) {
            DrumManager = GameObject.Find("DrumManager");
        }
        if (BassManager == null) {
            BassManager = GameObject.Find("BassManager");
        }
        if (PianoManager == null) {
            PianoManager = GameObject.Find("PianoManager");
        }
        if (pianotimer == null) {
            pianotimer = GameObject.Find("pianotimer");
        }

        if (DrumTutorial == null)
        {
            DrumTutorial = GameObject.Find("DrumTutorial");
        }

        if (DrumTutorial == null)
        {
            Debug.LogWarning("DrumTutorial not found");
        }
        else {
            DrumTutorial.SetActive(false);
        }

        if (BassTutorial == null)
        {
            BassTutorial = GameObject.Find("BassTutorial");
        }

        if (BassTutorial == null)
        {
            Debug.LogWarning("BassTutorial not found");
        }
        else {
            BassTutorial.SetActive(false);
        }

        if (PianoTutorial == null)
        {
            PianoTutorial = GameObject.Find("PianoTutorial");
        }

        if (PianoTutorial == null)
        {
            Debug.LogWarning("PianoTutorial not found");
        }
        else { 
            PianoTutorial.SetActive(false);
        }

        if (instrumentWheel == null)
        {
            instrumentWheel = GameObject.Find("InstrumentIcon");
        }
        
        if (instrumentWheel == null)
        {
            Debug.LogWarning("InstrumentIcon not found");
        }

        if (dtimer == null)
        {
            dtimer = GameObject.Find("Drumtimer");
        }

        if (dtimer == null)
        {
            Debug.LogWarning("drumtimer not found");
        }
        else
        {
            dtimer.SetActive(false);
        }

        if (pianotimer == null)
        {
            pianotimer = GameObject.Find("pianotimer");
        }
        
        if (pianotimer == null)
        {
            Debug.LogWarning("pianotimer not found");
        }
        else {
            pianotimer.SetActive(false);
        }

        instruments = new List<GameObject> { DrumManager, BassManager, PianoManager };
        instrumentAvailability = new List<bool> { isDrumAvailable, isBassAvailable, isPianoAvailable };

        foreach (GameObject instrument in instruments)
        {
            instrument.SetActive(false);
        }

        if (isDrumAvailable)
        {
            DrumManager.SetActive(true);
        }

        if (!isDrumAvailable && !isBassAvailable && !isPianoAvailable)
        {
            instrumentWheel.SetActive(false);
        }
        else
        {
            instrumentWheel.SetActive(true);
        }
    }
}