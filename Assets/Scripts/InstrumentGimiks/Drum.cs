using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    [SerializeField] private Renderer Dsphere;
    [SerializeField] private SphereCollider DrumCollider;
    [SerializeField] private Animator Animator;
    [SerializeField] private bool Drum_playing;
    // Start is called before the first frame update
    void Start()
    {
        Dsphere.enabled = false;
        DrumCollider.enabled = false;
        Drum_playing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !Drum_playing)
        {
            Drum_playing = true;
            Animator.SetBool("drumPlay", true);
            var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();
            Dsphere.enabled = true;
            DrumCollider.enabled = true;
            Invoke("RenderFalse",1.0f);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("calledDestroy");
            Destroy(other.gameObject, 0.2f);
        }  
    }

    public void RenderFalse() { 
        Dsphere.enabled = false;
        DrumCollider.enabled = false;
        Animator.SetBool("drumPlay", false);
        Invoke("CoolTime",5.0f);
    }

    public void CoolTime() {
        Drum_playing = false;
    }
}
