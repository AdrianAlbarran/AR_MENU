using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        _ObjectSpawned = false;
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();


    }

    private void Update()
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
                    _spawned_object.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
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
                    Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");

                    bool test = Physics.Raycast(ray, out hitObject);
                    if (test)
                    {
                        Debug.Log("Colisiono con: " + hitObject.transform.name);
                    }
                    else
                    {
                        Debug.Log(test);
                    }
                }
            }
        }
    }
}
