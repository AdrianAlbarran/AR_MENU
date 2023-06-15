using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Timer : MonoBehaviour
{
    // Clase para gestionar los cronometros
    public float timer = 0;
    public float starTime;

    public TextMeshProUGUI textTimer;

    public void Start()
    {
        timer = starTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        textTimer.text = "" + timer.ToString("f0");
    }

    public void OnEnable()
    {
        timer = starTime;
    }

    public void OnDisable()
    {
        textTimer.text = "00:00";
    }

}
