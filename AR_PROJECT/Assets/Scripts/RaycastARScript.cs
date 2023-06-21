using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
    public GameObject textCanvas;

    private bool foodSelected;
    [SerializeField] private RaycastARScriptDrinks drinksRaycastManager;

    private void Start()
    {
        foodSelected = false;
        _ObjectSpawned = false;
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();
        arCamera = Camera.main;

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

                    // activamos el analisis de la imagen
                    GetComponent<ARTrackedImageManager>().enabled = true;
                    GetComponent<TrackedImageInfoMultipleManager>().enabled = true;
                    textCanvas.GetComponent<TextMeshProUGUI>().text = "Escanea el qr";


                  
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

                    bool rayCollided = Physics.Raycast(ray, out hitObject);
                    if (rayCollided)
                    {
                        if(hitObject.transform.name== "campana" && TrackedImageInfoMultipleManager.imageDetected == true)
                        {
                            if(!foodSelected)
                            {
                                foodSelected = true;
                                // Quitamos la capacidad de hacer swipe
                                GameObject.FindObjectOfType<RestuaranteManager>().OnBellTouch();
                                drinksRaycastManager.enabled = true;
                                
                            }
                            else
                            {
                                try { GameObject.FindObjectOfType<SwipeDetector>().enabled = false; } catch { Debug.LogWarning("No se ha encontrado el swipe Detector"); }
                                gameCanvas.SetActive(true);
                            }

                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if(!drinksRaycastManager.enabled)
            addObject();

    }


}
