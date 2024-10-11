using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGround : MonoBehaviour
{
    private float switchtime = 3.0f;
    private float timelapse;

    //[SerializeField] private Renderer This;
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

        if (timelapse >= switchtime)
        {
            if (this.gameObject.activeSelf == true)
            {
                this.gameObject.SetActive(false);
                timelapse = 0.0f;
            }

            else
            {
                this.gameObject.SetActive(true);
                timelapse = 0.0f;
            }
            //timelapse = 0.0f;
        }
    }

}
