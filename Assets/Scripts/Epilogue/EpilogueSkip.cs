using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpilogueSkip : MonoBehaviour
{

    // Use this for initialization
    public void OnClick()
    {
        Invoke("ChangeScene", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeScene()
    {
        SceneManager.LoadScene("StaffRoll");
    }
}
