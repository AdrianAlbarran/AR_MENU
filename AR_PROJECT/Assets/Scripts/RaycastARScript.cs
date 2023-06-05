using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class RaycastARScript : MonoBehaviour
{
    public GameObject spawn_prefab;
    private GameObject _spawned_object;
    private bool _ObjectSpawned;
    public ARPlaneManager planeManager;

    [SerializeField]
    public Camera arCamera;

    private ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject gameCanvas;

    private void Start()
    {
        _ObjectSpawned = false;
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();


    }

    public void addObject()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (!_ObjectSpawned)
                {
                    Vector3 newPos = new Vector3(hitPose.position.x, hitPose.position.y + 0.05f, hitPose.position.z);
                    _spawned_object = Instantiate(spawn_prefab, hitPose.position, hitPose.rotation);
                    _spawned_object.name = "campana";
                    _ObjectSpawned = true;

                    foreach (var plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);

                        planeManager.enabled = false;

                    }

                }
                else
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;

                    bool test = Physics.Raycast(ray, out hitObject);
                    if (test)
                    {
                        Debug.Log("Colisiono con: " + hitObject.transform.name);
                        if(hitObject.transform.name== "campana")
                        {
                            gameCanvas.SetActive(true);
                        }
                    }
                    else
                    {
                        Debug.Log(test);
                    }
                }
            }
        }
    }
    private void Update()
    {
        addObject(); 
    }


}
