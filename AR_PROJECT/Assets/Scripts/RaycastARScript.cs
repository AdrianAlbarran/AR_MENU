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

    ARRaycastManager arrayMan;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        _ObjectSpawned = false;
        arrayMan = GetComponent<ARRaycastManager>();

    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (arrayMan.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;
                if (!_ObjectSpawned)
                {
                    Vector3 newPos = new Vector3(hitPose.position.x, hitPose.position.y + 0.05f, hitPose.position.z); 
                    _spawned_object = Instantiate(spawn_prefab, hitPose.position, hitPose.rotation);
                    _ObjectSpawned = true;
                }
                else
                {
                    _spawned_object.transform.position = hitPose.position;
                }
            }
        }
    }
}
