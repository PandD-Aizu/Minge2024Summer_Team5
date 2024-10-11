using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EpilogueManager : MonoBehaviour
{
    public GameObject[] imageObjects;  // 画像のGameObjectリスト（5つ）
    public GameObject[] textObjects;   // テキストのGameObjectリスト（13個）
    public float fadeDuration = 1.0f;  // フェードイン・アウトにかかる時間
    public float displayDuration = 2.0f;  // テキストを表示する時間

    private void Start()
    {
        // 全ての画像とテキストを初期状態で不可視化
        InitializeVisibility();

        // エピローグの表示を開始
        StartCoroutine(DisplayEpilogue());
    }

    private void InitializeVisibility()
    {
        // 全ての画像を透明にして非表示にする
        foreach (GameObject imageObject in imageObjects)
        {
            Image imageComponent = imageObject.GetComponent<Image>();
            imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, 0f);
        }

        // 全てのテキストを透明にして非表示にする
        foreach (GameObject textObject in textObjects)
        {
            Text textComponent = textObject.GetComponent<Text>();
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, 0f);
        }
    }

    private IEnumerator DisplayEpilogue()
    {
        // 画像1とテキスト1
        yield return StartCoroutine(ShowImageAndTexts(0, new int[] { 0 }));

        // 画像2とテキスト2,3,4
        yield return StartCoroutine(ShowImageAndTexts(1, new int[] { 1, 2, 3 }));

        // 画像3とテキスト5,6
        yield return StartCoroutine(ShowImageAndTexts(2, new int[] { 4, 5 }));

        // テキスト6,7,8,9（画像なし）
        yield return StartCoroutine(ShowTextsOnly(new int[] { 6, 7, 8, 9 }));

        // 画像4とテキスト7,8,9,10
        yield return StartCoroutine(ShowImageAndTexts(3, new int[] { 10, 11 }));

        // 画像5とテキスト11,12
        yield return StartCoroutine(ShowImageAndTexts(4, new int[] { 12 }));


        // プロローグが終わったらTitleSceneに遷移
        SceneManager.LoadScene("Title");
    }

    private IEnumerator ShowImageAndTexts(int imageIndex, int[] textIndices)
    {
        // 画像のフェードイン
        yield return StartCoroutine(FadeIn(imageObjects[imageIndex]));

        // テキストを順にフェードイン表示
        foreach (int textIndex in textIndices)
        {
            yield return StartCoroutine(FadeIn(textObjects[textIndex]));
        }

        // 表示時間待機
        yield return new WaitForSeconds(displayDuration);

        // テキストをフェードアウト
        foreach (int textIndex in textIndices)
        {
            yield return StartCoroutine(FadeOut(textObjects[textIndex]));
        }

        // 画像のフェードアウト
        yield return StartCoroutine(FadeOut(imageObjects[imageIndex]));
    }

    private IEnumerator ShowTextsOnly(int[] textIndices)
    {
        // テキストを順にフェードイン表示
        foreach (int textIndex in textIndices)
        {
            yield return StartCoroutine(FadeIn(textObjects[textIndex]));
        }

        // 表示時間待機
        yield return new WaitForSeconds(displayDuration);

        // テキストをフェードアウト
        foreach (int textIndex in textIndices)
        {
            yield return StartCoroutine(FadeOut(textObjects[textIndex]));
        }
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        Graphic graphic = obj.GetComponent<Graphic>();  // ImageかTextコンポーネントを取得
        Color originalColor = graphic.color;
        originalColor.a = 0f;
        graphic.color = originalColor;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        Graphic graphic = obj.GetComponent<Graphic>();  // ImageかTextコンポーネントを取得
        Color originalColor = graphic.color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        graphic.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
