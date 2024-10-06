using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Start_button : MonoBehaviour
{
    public GameObject BackGroundImage2;
    public GameObject GameStartButton;
    public GameObject GameEndButton;
    public GameObject TitleLogo;
    public GameObject PlayerImage;

    public float slideDistance = 500f;  // スライドさせる距離
    public float fadeDuration = 1.5f;   // フェードにかける時間
    public float slideSpeed = 200f;     // スライド速度

    private bool isTransitioning = false; // 遷移中かどうかを判断するためのフラグ

    // Start is called before the first frame update
    void Start()
    {
        BackGroundImage2 = GameObject.Find("TitleBackGround2");
        GameStartButton = GameObject.Find("StartButton");
        GameEndButton = GameObject.Find("EndButton");
        TitleLogo = GameObject.Find("Title");
        PlayerImage = GameObject.Find("PlayerImage");
    }

    // Update is called once per frame
    void Update()
    {
        if (isTransitioning)
        {
            // BackGroundImage2を右にスライドさせる
            BackGroundImage2.transform.Translate(Vector3.right * slideSpeed * Time.deltaTime);

            // スライド終了判定
            if (BackGroundImage2.transform.localPosition.x >= slideDistance)
            {
                isTransitioning = false;
                SceneManager.LoadScene("PrologueScene", LoadSceneMode.Single);  // シーン遷移
            }
        }
    }

    public void PushedStartButton()
    {
        StartCoroutine(FadeOutAndSlide());
    }

    IEnumerator FadeOutAndSlide()
    {
        isTransitioning = true;

        // フェード処理
        StartCoroutine(FadeOut(GameStartButton));
        StartCoroutine(FadeOut(GameEndButton));
        StartCoroutine(FadeOut(TitleLogo));
        StartCoroutine(FadeOut(PlayerImage));

        // フェードが終わるまで待つ
        yield return new WaitForSeconds(fadeDuration);
    }

    IEnumerator FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            // CanvasGroupがなければ追加
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        float startAlpha = canvasGroup.alpha;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0;  // 完全に透明にする
    }

    public void PushedEndButton()
    {
        Application.Quit();
    }
}
