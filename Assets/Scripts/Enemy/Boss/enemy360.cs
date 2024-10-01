using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy360 : MonoBehaviour
{
    public Shot m_shotPrefab;//’e‚ÌƒvƒŒƒnƒu
    public Shot m_shotPrefab2;// ’e‚ÌƒvƒŒƒnƒu
    public float m_shotSpeed; // ’e‚ÌˆÚ“®‚Ì‘¬‚³
    public float m_shotAngleRange = 360; // •¡”‚Ì’e‚ğ”­Ë‚·‚é‚ÌŠp“x
    public int m_shotCount; // ’e‚Ì”­Ë”
    public float m_shotSecondshot = 0.75f; // ‚QË‚ß‚Ü‚Å‚ÌŠÔŠu
    public float m_shotrensya = 2; // ˜AË‚Ì‰ñ”
    public float m_shotInterval = 3f; // ’e‚Ì”­ËŠÔŠui•bj

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Shottime());
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public IEnumerator Shottime()
    {
            for (int i = 0; i < m_shotrensya; i++)
            {
                ShootNWay(360, m_shotAngleRange, m_shotSpeed, m_shotCount);
                yield return new WaitForSeconds(m_shotSecondshot);
            }
            
    }
    private void ShootNWay(
    float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // ƒvƒŒƒCƒ„[‚ÌˆÊ’u
        var rot = transform.localRotation; // ƒvƒŒƒCƒ„[‚ÌŒü‚«

        // ’e‚ğ•¡””­Ë‚·‚éê‡
        if (1 < count)
        {
            // ”­Ë‚·‚é‰ñ”•ªƒ‹[ƒv‚·‚é
            for (int i = 0; i < count; ++i)
            {
                // ’e‚Ì”­ËŠp“x‚ğŒvZ‚·‚é
                var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);

                int rnd = Random.Range(1, 101);

                    if (rnd < 50)
                    {
                        //Debug.Log("50‚æ‚èã");
                        // ”­Ë‚·‚é’e‚ğ¶¬‚·‚é
                       var shot = Instantiate(m_shotPrefab, pos, rot);
                        // ’e‚ğ”­Ë‚·‚é•ûŒü‚Æ‘¬‚³‚ğİ’è‚·‚é
                       shot.Init(angle, speed);
                    }

                    else
                    {
                        //Debug.Log("50ˆÈ‰º");
                        var shot = Instantiate(m_shotPrefab2, pos, rot);
                        shot.Init(angle, speed);
                    }               

                //‚±‚±‚Å’e’Ç‰Á@ƒ‰ƒ“ƒ_ƒ€‚È”‚ğo—Í@if•¶‚Åˆê’è‚Ì”ˆÈã‚È‚ç‚±‚Ì‹…‚İ‚½‚¢‚É‚µ‚Ä‚Qí—Ş‚¾‚·@’e‚ÌƒvƒŒƒnƒu‚à’Ç‰Á
            }
        }
        /*// ’e‚ğ 1 ‚Â‚¾‚¯”­Ë‚·‚éê‡
        else if (count == 1)
        {
            // ”­Ë‚·‚é’e‚ğ¶¬‚·‚é
            var shot = Instantiate(m_shotPrefab, pos, rot);

            // ’e‚ğ”­Ë‚·‚é•ûŒü‚Æ‘¬‚³‚ğİ’è‚·‚é
            shot.Init(angleBase, speed);
        }*/
    }
    
}
