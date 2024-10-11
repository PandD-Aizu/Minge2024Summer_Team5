using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGround : MonoBehaviour
{
    private float switchtime1 = 3.0f;
    private float switchtime2 = 6.0f;
    private float timelapse;
    [SerializeField] GameObject SwitchGround1;
    [SerializeField] GameObject SwitchGround2;
    [SerializeField] GameObject SwitchGround3;
    [SerializeField] GameObject SwitchGround4;
    [SerializeField] GameObject SwitchGround5;
    [SerializeField] GameObject SwitchGround6;
    [SerializeField] GameObject SwitchGround7;
    

    // Start is called before the first frame update
    void Start()
    {
        timelapse = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timelapse += Time.deltaTime;
        Debug.Log(timelapse);

        if (timelapse >= switchtime1)
        {
            SwitchGround1.SetActive(false);
            SwitchGround2.SetActive(false);
            SwitchGround3.SetActive(true);
            SwitchGround4.SetActive(false);
            SwitchGround5.SetActive(false);
            SwitchGround6.SetActive(true);
            SwitchGround7.SetActive(true);
        }

        if (timelapse >= switchtime2)
        {
            SwitchGround1.SetActive(true);
            SwitchGround2.SetActive(true);
            SwitchGround3.SetActive(false);
            SwitchGround4.SetActive(true);
            SwitchGround5.SetActive(true);
            SwitchGround6.SetActive(false);
            SwitchGround7.SetActive(false);
            timelapse = 0.0f;
        }
    }

}
