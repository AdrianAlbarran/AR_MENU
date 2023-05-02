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
        if(swipeData.Direction == SwipeDirection.Left)
        {
            modelsArray[indexActivo].gameObject.SetActive(false);
            --indexActivo;
            indexActivo = indexActivo < 0 ? modelsArray.Count - 1 : indexActivo;
            modelsArray[indexActivo].gameObject.SetActive(true);
            Debug.LogWarning("Swipe hacia la izquierda, para cambiar objeto");
        }
        else
        {
            modelsArray[indexActivo].gameObject.SetActive(false);
            indexActivo = (indexActivo + 1) % modelsArray.Count;
            Debug.LogWarning("Swipe hacia la derecha, para cambiar objeto");
            modelsArray[indexActivo].gameObject.SetActive(true);
        }
    }

    public GameObject CurrentModel()
    {
        return modelsArray[indexActivo];
    }


}
