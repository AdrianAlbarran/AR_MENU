using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class RaycastARScriptGame : MonoBehaviour
{
    public GameObject spawn_prefab;
    private GameObject _spawned_object;
    private bool _ObjectSpawned;
    public ARPlaneManager planeManager;
    public GameObject gameButton;
    public GameObject areaButton;

    public bool canSpawn;

    [SerializeField]
    public Camera arCamera;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

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

                Vector3 newPos = new Vector3(hitPose.position.x, hitPose.position.y + 0.05f, hitPose.position.z);
                _spawned_object = Instantiate(spawn_prefab, hitPose.position, hitPose.rotation);
                _spawned_object.name = "Game";
                _ObjectSpawned = true;
                canSpawn = false;
                UIEnabler(true);

                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);

                    planeManager.enabled = false;
                }

                //else
                //{
                //    Ray ray = arCamera.ScreenPointToRay(touch.position);
                //    RaycastHit hitObject;

                //    bool test = Physics.Raycast(ray, out hitObject);
                //    if (test)
                //    {
                //        Debug.Log("Colisiono con: " + hitObject.transform.name);
                //    }
                //    else
                //    {
                //        Debug.Log(test);
                //    }
                //}
            }
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            addObject();
        }
    }

    public void Delete()
    {
        if (_ObjectSpawned)
        {
            Destroy(_spawned_object);
        }
    }

    public void Spawn()
    {
        Delete();
        UIEnabler(false);
        canSpawn = true;
    }

    public void StartGame()
    {
        Debug.Log("Empezar io que se");
    }

    public void UIEnabler(bool control)
    {
        if (control)
        {
            areaButton.SetActive(true);
            gameButton.SetActive(true);
        }
        else
        {
            areaButton.SetActive(false);
            gameButton.SetActive(false);
        }
    }
}