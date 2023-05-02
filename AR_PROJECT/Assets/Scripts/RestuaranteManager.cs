using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestuaranteManager : MonoBehaviour
{

    [SerializeField]
    List<GameObject> modelsArray;

    int indexActivo;

    public void Awake()
    {
        SwipeDetector.OnSwipe += changeModelOnSwipe;
    }

    private void changeModelOnSwipe(SwipeData swipeData)
    {
        if(swipeData.Direction == SwipeDirection.Left)
        {
            --indexActivo;
            indexActivo = indexActivo < 0 ? modelsArray.Count - 1 : indexActivo;
            Debug.LogWarning("Swipe hacia la izquierda, para cambiar objeto");
        }
        else
        {
            indexActivo = (indexActivo + 1) % modelsArray.Count;
            Debug.LogWarning("Swipe hacia la derecha, para cambiar objeto");
        }
    }
}
