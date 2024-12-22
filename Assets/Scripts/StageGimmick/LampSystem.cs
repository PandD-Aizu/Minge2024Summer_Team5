using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSystem : MonoBehaviour
{
    public SwitchCounter SwitchCounter;
    public bool this_object_is_lamp_on;//kono object no mitame wo hantei suru
    public int lamp_num;//kono object no tentou suru junban wo hantei suru
    private Vector3 pos;
    private Transform myTransform;
    public bool lamp_on = true;

    // Start is called before the first frame update
    void Start()
    {

        SwitchCounter = GameObject.FindObjectOfType<SwitchCounter>();
        if (this_object_is_lamp_on == true)
        {
            this.gameObject.SetActive(true);
        }
        else if (this_object_is_lamp_on == false)
        {
            this.gameObject.SetActive(true);
        }
    }

    //memo:object‚ª”ñactive‚Ì‚Æ‚«,Update‚ªŒÄ‚Î‚ê‚È‚­‚È‚é

    // Update is called once per frame
    void Update()
    {
        if(this_object_is_lamp_on == true)//kono object ha lamp ga tuiteiru mitame
        {
            
        }
        else if(this_object_is_lamp_on == false)//kono object ha lamp ga tuiteinai mitame
        {
            if (SwitchCounter.switchcount >= lamp_num && lamp_on == true)//lamp off ni
            {
                myTransform = this.transform;
                pos = myTransform.position;
                pos.z += 5.0f;
                myTransform.position = pos;
                lamp_on = false;
            }
            else if(SwitchCounter.switchcount < lamp_num && lamp_on == false)//lamp on ni
            {
                myTransform = this.transform;
                pos = myTransform.position;
                pos.z -= 5.0f;
                myTransform.position = pos;
                lamp_on = true;
            }
        }


    
        
    }
}
