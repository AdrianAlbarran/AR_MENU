using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RestuaranteManager : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> modelsArray;

    int indexActivo;

    public void Awake()
    {
        indexActivo = 0;
        SwipeDetector.OnSwipe += changeModelOnSwipe;
        foreach (GameObject model in modelsArray)
        {
            model.SetActive(false);
        }
    }

    private void changeModelOnSwipe(SwipeData swipeData)
    {
        if(!TrackedImageInfoMultipleManager.imageDetected) { return; }
        if(swipeData.Direction == SwipeDirection.Left)
        {
            modelsArray[indexActivo].gameObject.SetActive(false);
            --indexActivo;
            indexActivo = indexActivo < 0 ? modelsArray.Count - 1 : indexActivo;
            modelsArray[indexActivo].gameObject.SetActive(true);
        }
        else
        {
            modelsArray[indexActivo].gameObject.SetActive(false);
            indexActivo = (indexActivo + 1) % modelsArray.Count;
            modelsArray[indexActivo].gameObject.SetActive(true);
        }
    }

    public GameObject CurrentModel()
    {
        return modelsArray[indexActivo];
    }

    public void OnBellTouch()
    {
        SwipeDetector.OnSwipe -= changeModelOnSwipe;
        CurrentModel().GetComponentInChildren<ModelController>().enabled = false;
    }
}
