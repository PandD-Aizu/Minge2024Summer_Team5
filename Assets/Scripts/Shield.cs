using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] private Renderer Dshield;
    [SerializeField] private Animator Animator;
    [SerializeField] private bool Shield_playing;

    // Start is called before the first frame update
    void Start()
    {

        Dshield.enabled = true;
        Shield_playing = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.R) && !Shield_playing)
        {
            Dshield.enabled = true;
            Animator.SetBool("shieldPlay", true);
            Shield_playing = true;
            Invoke("RenderFalse", 5.0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject, 0.1f);
        }
    }

    public void RenderFalse()
    {
        Dshield.enabled = true;
        Animator.SetBool("shieldPlay", false);
        Invoke("CoolTime", 5.0f);
    }

    public void CoolTime()
    {
        Shield_playing = false;
    }
}
