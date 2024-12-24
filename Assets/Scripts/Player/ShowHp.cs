using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHp : MonoBehaviour
{
    public GameObject heartPrefab;// ハートのプレハブ（UIのハート画像）
    public GameObject breakedheart;
    public Transform heartsPanel; // ハートを配置するパネル
    public int maxhp = 3;
    private List<GameObject> hearts = new List<GameObject>(); // 表示中のハートを管理するリスト


    public void ResetHealth(int maxhp)
    {
        // ハートを更新
        UpdateHearts(maxhp);
    }

    public void UpdateHearts(int currenthp)
    {
        // 既存のハートを一度全て削除
        foreach (GameObject heart in hearts)
        {
            Destroy(heart);
        }

        hearts.Clear();

        for (int i = 0; i < maxhp; i++)
        {
            if (i < currenthp)
            {
                GameObject heart = Instantiate(heartPrefab, heartsPanel);
                hearts.Add(heart);
            }
            else
            {
                GameObject damagedHeart = Instantiate(breakedheart, heartsPanel);
                hearts.Add(damagedHeart);
            }
        }
    }

    public void DamagedHearts(int damage, int currenthp)
    {
        // 現在のリストに基づいて、ダメージ分のハートを差し替え
        for (int i = 0; i < damage; i++)
        {
            int indexToReplace = currenthp + i; // 体力が減った後のインデックス位置

            if (indexToReplace < maxhp && hearts[indexToReplace] != null)
            {
                Destroy(hearts[indexToReplace]); // 現在のハートを削除
                GameObject damagedHeart = Instantiate(breakedheart, heartsPanel);
                hearts[indexToReplace] = damagedHeart; // ダメージハートを置き換え
            }
        }
    }
}

