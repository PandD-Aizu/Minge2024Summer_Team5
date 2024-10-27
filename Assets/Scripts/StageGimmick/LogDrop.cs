using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDrop : MonoBehaviour
{
    [SerializeField] GameObject LogPrefab;
    private float shoottime = 3.0f;
    private float timelapse;
    //private bool shoot = false;
    public GameObject Log;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        rot = Quaternion.Euler(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        timelapse += Time.deltaTime;

        if(timelapse >= shoottime)
        {
            Log = Instantiate(LogPrefab, this.gameObject.transform.position, rot);
            timelapse = 0.0f;
        }
    }
}
