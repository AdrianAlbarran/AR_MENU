using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastARScriptDrinks : MonoBehaviour
{
    public GameObject spawn_prefab;
    private GameObject _spawned_object;
    private bool _ObjectSpawned;
    public ARPlaneManager planeManager;

    [SerializeField]
    public Camera arCamera;

    private ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public GameObject textCanvas;

    private void OnEnable()
    {
        _ObjectSpawned = false;
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        StartCoroutine(EnablePlanes());

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
                    Vector3 newPos = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);
                    _spawned_object = Instantiate(spawn_prefab, hitPose.position, hitPose.rotation);
                    _ObjectSpawned = true;

                    foreach (var plane in planeManager.trackables)
                    {
                        plane.gameObject.SetActive(false);

                        planeManager.enabled = false;
                    }

                    GetComponent<RaycastARScriptDrinks>().enabled = false;
                }
            }
        }
    }

    private void Update()
    {
        addObject();
    }

    IEnumerator EnablePlanes()
    {
        yield return new WaitForSeconds(0.3f);
        
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true);

            planeManager.enabled = true;
        }
    }

}
