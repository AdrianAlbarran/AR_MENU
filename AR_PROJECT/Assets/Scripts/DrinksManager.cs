using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinksManager : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> modelsArray;

    int indexActivo;

    public void Start()
    {
        indexActivo = 0;
        SwipeDetector.OnSwipe += changeModelOnSwipe;
        foreach (GameObject model in modelsArray)
        {
            model.SetActive(false);
        }
        modelsArray[indexActivo].SetActive(true);
    }

    private void changeModelOnSwipe(SwipeData swipeData)
    {
        if (!TrackedImageInfoMultipleManager.imageDetected) { return; }
        if (swipeData.Direction == SwipeDirection.Left)
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
}
