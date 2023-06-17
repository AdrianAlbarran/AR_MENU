using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoMultipleManager : MonoBehaviour
{
    // Modelos a imprimir
    [SerializeField]
    private GameObject[] arObjectsToPlace;

    private ARTrackedImageManager m_TrackedImageManager;

    private Dictionary<string, GameObject> arObjects = new Dictionary<string, GameObject>();

    // Delegado para cuando detectamos un qr/imagen
    public static Action onDetection;

    public static bool imageDetected = false;

    [SerializeField] GameObject textCanvasUI;
    void Awake()
    {
        //dismissButton.onClick.AddListener(Dismiss);
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();

        // setup all game objects in dictionary
        foreach (GameObject arObject in arObjectsToPlace)
        {
            GameObject newARObject = Instantiate(arObject, Vector3.zero, Quaternion.identity);
            newARObject.name = arObject.name;
            // Guardas en el diccionario el gameobject del modelo
            arObjects.Add(arObject.name, newARObject);
        }
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

 
    //private void Dismiss() => welcomePanel.SetActive(false);

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            imageDetected = true;
            UpdateARImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                imageDetected = true;
                UpdateARImage(trackedImage);
            }

            //TENER CUIDADO CON CUANDO NO ESTE ANALIZANDO UNA IMAGEN CONCRETA, PARECE QUE CAPTA ALGUNA CON IDENTIFICADOR RANDOM
            // else {arObjects[trackedImage.name].SetActive(false); } Esto da errores   
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            arObjects[trackedImage.name].GetComponent<RestuaranteManager>().CurrentModel().SetActive(false);
            imageDetected = false;
        }

    }

    private void UpdateARImage(ARTrackedImage trackedImage)
    {
        // Display the name of the tracked image in the canvas
        //imageTrackedText.text = trackedImage.referenceImage.name;

        // Assign and Place Game Object
        AssignGameObject(trackedImage.referenceImage.name, trackedImage.transform.position);
    }

    void AssignGameObject(string name, Vector3 newPosition)
    {
        if (arObjectsToPlace != null)
        {
            textCanvasUI.SetActive(false);
            GameObject goARObject = arObjects[name].GetComponent<RestuaranteManager>().CurrentModel();
            goARObject.SetActive(true);
            goARObject.transform.position = newPosition;

            //foreach (GameObject go in arObjects.Values)
            //{
            //    Debug.Log($"Go in arObjects.Values: {go.name}");
            //    if (go.name != name)
            //    {
            //        go.SetActive(false);
            //    }
            //}
        }
    }
}