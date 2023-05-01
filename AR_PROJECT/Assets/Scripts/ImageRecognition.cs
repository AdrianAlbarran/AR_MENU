using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecognition : MonoBehaviour
{
    // Start is called before the first frame update

    private ARTrackedImageManager _arTrackerImageManager;

    private void Awake()
    {
        _arTrackerImageManager = FindObjectOfType<ARTrackedImageManager>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        _arTrackerImageManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _arTrackerImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
        }
    }
}
