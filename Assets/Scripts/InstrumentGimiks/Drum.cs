using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Drum : MonoBehaviour
{
    [SerializeField] private Renderer Dsphere;
    [SerializeField] private SphereCollider DrumCollider;
    [SerializeField] private Animator Animator;
    [SerializeField] public bool Drum_playing;
    [SerializeField] private AudioSource Drumaudio;
    [SerializeField] private AudioClip Drumsound;
    // Start is called before the first frame update
    void Start()
    {
        Dsphere.enabled = false;
        DrumCollider.enabled = false;
        Drum_playing = false;
        Drumaudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !Drum_playing && Time.timeScale == 1)
        {
            Drumaudio.PlayOneShot(Drumsound);
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
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Dust"))
        {
            Debug.Log("calledDestroy");
            Destroy(other.gameObject, 0.2f);
        }

        if (other.gameObject.name == "Log(Clone)")
        {
            Destroy(other.gameObject);
        }
    }

    public void RenderFalse() { 
        Dsphere.enabled = false;
        DrumCollider.enabled = false;
        Animator.SetBool("drumPlay", false);
        //Invoke("CoolTime",5.0f);
        Debug.Log("クールタイム開始");
    }

    public void CoolTime() {
        Drum_playing = false;
        Debug.Log("クールタイム終わり");
    }
}
