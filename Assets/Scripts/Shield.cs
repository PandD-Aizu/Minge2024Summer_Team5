using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private Renderer Dshield;

    // Start is called before the first frame update
    void Start()
    {

        Dshield.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Dshield.enabled = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Dshield.enabled = false;
        }
    }
}
