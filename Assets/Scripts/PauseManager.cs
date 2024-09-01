using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // ポーズ画面
    private bool isPaused = false; // ポーズ中かどうかのフラグ

    private void Start()
    {
        pauseMenu = GameObject.Find("PauseCanvas");
        pauseMenu.SetActive(false);
    }
    void Update()
    {
        // ESCキーが押された場合、ポーズの切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // ゲームを一時停止する
    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // ゲームを停止
        isPaused = true;
    }

    // ゲームを再開する
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // ゲームを再開
        isPaused = false;
    }

    // ステージセレクトシーンに移動する
    public void GoToStageSelect()
    {
        Time.timeScale = 1f; // ゲームを再開してからシーン遷移
        SceneManager.LoadScene("StageSelectScene");
    }
}
