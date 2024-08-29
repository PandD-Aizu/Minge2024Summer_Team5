using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField] private Renderer Dsphere;
    [SerializeField] private Animator Animator;
    [SerializeField] private bool Drum_playing;
    // Start is called before the first frame update
    void Start()
    {
        Dsphere.enabled = false;
        Drum_playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !Drum_playing)
        {
            Drum_playing = true;
            Animator.SetBool("drumPlay", true);
            Dsphere.enabled = true;
            Invoke("RenderFalse",2.0f);
        }
        
    }

    public void RenderFalse() {
        Drum_playing = false;
        Dsphere.enabled = false;
        Animator.SetBool("drumPlay", false);
    }
}
