using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrumentManager : MonoBehaviour
{
    // ?V???O???g???C???X?^???X
    private static InstrumentManager instance;
    public GameObject DrumManager;
    public GameObject BassManager;
    public GameObject PianoManager;

    // ?y?????`???[?g???A???I?u?W?F?N?g
    public GameObject DrumTutorial;
    public GameObject BassTutorial;
    public GameObject PianoTutorial;

    // ?y???????p???\?t???O
    public bool isDrumAvailable = false;
    public bool isBassAvailable = false;
    public bool isPianoAvailable = false;

    private bool wasDrumAvailable = false;
    private bool wasBassAvailable = false;
    private bool wasPianoAvailable = false;

    // ?????I???????y???C???f?b?N?X (0: Drum, 1: Bass, 2: Piano)
    private int currentInstrumentIndex = 0;
    private List<GameObject> instruments;
    private List<bool> instrumentAvailability;

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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("?V?[???????[?h??????????: " + scene.name);
        if (scene.name != "Title" && scene.name != "StageSelectScene")
        {
            DrumManager = GameObject.Find("DrumManager");
            BassManager = GameObject.Find("BassManager");
            PianoManager = GameObject.Find("PianoManager");

            DrumTutorial = GameObject.Find("DrumTutorial");
            BassTutorial = GameObject.Find("BassTutorial");
            PianoTutorial = GameObject.Find("PianoTutorial");

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
        }
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
        if (isDrumAvailable && !wasDrumAvailable)
        {
            DrumTutorial.SetActive(true);
            Time.timeScale = 0f;
            //Debug.Log("?h?????`???[?g???A?????\??");
        }

        if (isBassAvailable && !wasBassAvailable)
        {
            BassTutorial.SetActive(true);
            Time.timeScale = 0f;
            //Debug.Log("?x?[?X?`???[?g???A?????\??");
        }

        if (isPianoAvailable && !wasPianoAvailable)
        {
            PianoTutorial.SetActive(true);
            Time.timeScale = 0f;
            //Debug.Log("?s?A?m?`???[?g???A?????\??");
        }

        // ???????X?V
        wasDrumAvailable = isDrumAvailable;
        wasBassAvailable = isBassAvailable;
        wasPianoAvailable = isPianoAvailable;
    }
}
