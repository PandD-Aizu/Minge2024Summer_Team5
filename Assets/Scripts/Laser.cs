using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Leaser : MonoBehaviour
{
    [Header("Settings")]
    public LayerMask layerMask;
    public float defaultLength = 50;
    public int numOfReflections = 2;

    private LineRenderer _lineRenderer;
    private Camera _myCam;
    private RaycastHit hit;

    private Ray ray;
    private Vector3 direction;
    private GameObject SwitchObject;
    private GameObject area;
    private DoorControler doorContoroler;
    private SphereMove Smove;
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ReflectLaser();
    }

    void ReflectLaser()
    {
        ray = new Ray(transform.position, transform.forward);

        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, transform.position);

        float remainLength = defaultLength;


        for (int i = 0; i < numOfReflections; i++)
        {
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainLength, layerMask))
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, hit.point);

                if (hit.collider.CompareTag("Switch"))
                {
                    Debug.Log("Switch");
                    doorContoroler = GameObject.FindGameObjectWithTag("Switch").GetComponent<DoorControler>();
                    doorContoroler.Door();
                    break;  // タグが一致したらループを終了
                }
                else if(hit.collider.CompareTag("ElevatorSwitch"))
                {
                    Debug.Log("ElevatorSwitch");
                    area = GameObject.Find("Area");
                    area.GetComponent<Collider>().enabled = true;
                    break;  // タグが一致したらループを終了
                }
                  
                remainLength -= Vector3.Distance(ray.origin, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                _lineRenderer.positionCount += 1;
                _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, ray.origin + (ray.direction * remainLength));
                break;
            }
        }
    }
}

    /*
    void NormalLaser()
    {
        _lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }
    */