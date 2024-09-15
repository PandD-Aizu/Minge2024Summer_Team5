using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour
{

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("log");
        //プレイヤーに当たったらプレイヤーを倒す
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ki");
            //Destroy(other.gameObject, 1.0f);
            Invoke("playerResporn", 0.1f);
        }
    }

    private void playerResporn()
    {
        player.playerResporn();
    }

}
