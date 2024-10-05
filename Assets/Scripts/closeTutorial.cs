using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class closeTutorial : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData Data)
    {
        GameObject Parent = gameObject.transform.parent.gameObject;
        Parent.gameObject.SetActive(false);
        Time.timeScale = 1f;  // ゲーム内時間を再開
        Debug.Log("チュートリアルを閉じ、時間を再開");
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey (KeyCode.Escape))
        {
            GameObject Parent = gameObject.transform.parent.gameObject;
            Parent.gameObject.SetActive(false);
            Time.timeScale = 1f;  // ゲーム内時間を再開
            Debug.Log("チュートリアルを閉じ、時間を再開");
        }
    }
}
