using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Drumtimer : MonoBehaviour
{
    GameObject canvas;
    GameObject icon1;
    GameObject icon2;
    Image gauge1;
    Image gauge2;

    Drum drum;
    Shield shield;
    InstrumentManager instrument;

    public float impact_cooltime = 6.0f;
    public float impact_cooltime_define = 6.0f;
    public float shield_cooltime = 6.0f;
    public float shield_cooltime_define = 6.0f;

    // Start is called before the first frame update
    void Start()
    {
        drum = GameObject.FindObjectOfType<Drum>();
        shield = GameObject.FindObjectOfType<Shield>();
        instrument = GameObject.FindObjectOfType<InstrumentManager>();

        canvas = GameObject.Find("Drumtimer");
        if (canvas == null)
        {
            Debug.LogWarning("Drumtimerが設置されていません");
        }
        gauge1 = GameObject.Find("impact").GetComponent<Image>();    
        if (gauge1 == null)
        {
            Debug.LogWarning("impactが設置されていません");
        }
        else
        {
            Debug.Log("kieroooooo");
            gauge1.enabled = false;        
            gauge1.fillAmount = 1f;
        }
        icon1 = GameObject.Find("impact-icon");
        if (icon1 == null)
        {
            Debug.LogWarning("impct-iconが設置されていません");
        }
        else
        {
          //  icon1.SetActive(true);
        }
        gauge2 = GameObject.Find("shield").GetComponent<Image>();
        if (gauge2 == null)
        {
            Debug.LogWarning("shieldが設置されていません");
        }
        else
        {
            gauge2.enabled = false;
            gauge2.fillAmount = 1f;
        }
        icon2 = GameObject.Find("shield-icon");
        if (icon2 == null)
        {
            Debug.LogWarning("shield-iconが設置されていません");
        }
        else
        {
            //icon2.SetActive(true);
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if (drum.Drum_playing == true)
        {
            ImpactCT();
        }
        if (shield.Shield_playing == true)
        {
            ShieldCT();
        }
    }

    public void ImpactCT()
    {
        gauge1.enabled = true;
        //icon1.SetActive (true);
        gauge1.fillAmount = impact_cooltime / impact_cooltime_define;
        impact_cooltime -= Time.deltaTime;

        if (gauge1.fillAmount <= 0)
        {     
            drum.Drum_playing = false;
            gauge1.enabled = false;
            impact_cooltime = impact_cooltime_define;
            Debug.Log("owari");
        }
    }

    public void ShieldCT()
    {
        gauge2.enabled = true;
        //icon2.SetActive (true);
        gauge2.fillAmount = shield_cooltime / shield_cooltime_define;
        shield_cooltime -= Time.deltaTime;

        if (gauge2.fillAmount <= 0)
        {
            shield.Shield_playing = false;
            gauge2.enabled = false;
            shield_cooltime = shield_cooltime_define;
            Debug.Log("owariiiiiiii");
        }
    }
}

