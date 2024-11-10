using UnityEngine;
using System.Collections;

public class gravityController : MonoBehaviour
{
    public GameObject Player;
    public Player oiuea;/*いい名前が思いつかない*/
    public Vector3 Anti_Gravity;
    public Vector3 Normal_Gravity;
    public int InZoneChecker = 0;

    private Rigidbody rb;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        oiuea = Player.GetComponent<Player>();
        rb = Player.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        setLocalGravity();
    }

    void OnTriggerStay(Collider other)/*オブジェクトが重なっている間*/
    {
        Debug.Log("Trigger Stay: " + other.gameObject.name);
        InZoneChecker = 1;
    }
    void OnTriggerExit(Collider other)/*オブジェクトが離れた時*/
    {
        Debug.Log("Trigger Exit: " + other.gameObject.name);
        InZoneChecker = 0;
    }


    void setLocalGravity()
    {
        if(InZoneChecker == 1)
        {
            rb.AddForce(Anti_Gravity, ForceMode.Acceleration);
            Debug.Log("G:Anti");
        }
        else if(InZoneChecker == 0)
        {
            rb.AddForce(Normal_Gravity, ForceMode.Acceleration);
            //Debug.Log("G:Normal");
        }
        

    }

}

