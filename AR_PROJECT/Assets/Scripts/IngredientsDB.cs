using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngredientsDB : MonoBehaviour
{
    public GameObject[] ingredients;
    public float price;
    public TextMeshProUGUI strPrice;

    public void Start()
    {
        strPrice.text = price.ToString() + "$";
    }
}
