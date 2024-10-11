using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip bgm1;  // 一つ目のBGM
    public AudioClip bgm2;  // 二つ目のBGM

    private AudioSource audioSource1;  // 一つ目のBGM用
    private AudioSource audioSource2;  // 二つ目のBGM用

    public float fadeDuration = 2.0f;  // フェードの持続時間
    public float overlapTime = 2.0f;   // 一つ目が終わる直前に二つ目をスタートさせる時間
    public float maxVolume = 0.6f;     // 最大音量

    void Start()
    {
        // 二つのAudioSourceを用意
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();

        // 一つ目のBGMを再生
        audioSource1.clip = bgm1;
        audioSource1.loop = false;
        audioSource1.volume = maxVolume;  // 初期音量を0.6に設定
        audioSource1.Play();

        StartCoroutine(CrossFadeBGM());
    }

    IEnumerator CrossFadeBGM()
    {
        // 一つ目のBGMの長さから重なり時間を引いた時間だけ待つ
        yield return new WaitForSeconds(bgm1.length - overlapTime);

        // 二つ目のBGMをフェードインしながら再生開始
        audioSource2.clip = bgm2;
        audioSource2.volume = 0;  // 初めは音量0
        audioSource2.loop = true;  // 二つ目のBGMはループする
        audioSource2.Play();

        // フェード処理
        float timer = 0;
        while (timer < fadeDuration)
        {
            audioSource1.volume = Mathf.Lerp(maxVolume, 0, timer / fadeDuration);  // 一つ目のBGMをフェードアウト
            audioSource2.volume = Mathf.Lerp(0, maxVolume, timer / fadeDuration);  // 二つ目のBGMをフェードイン
            timer += Time.deltaTime;
            yield return null;
        }

        // フェード完了後、完全に一つ目のBGMを停止
        audioSource1.Stop();
        audioSource1.volume = maxVolume;  // 初期音量を0.6に戻す
    }
}
