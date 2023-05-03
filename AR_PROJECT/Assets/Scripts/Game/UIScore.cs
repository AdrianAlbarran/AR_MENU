
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore :MonoBehaviour, IObserver<int>
{

    private PlayerController player;
    public TextMeshProUGUI scoreText;

    public void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        player.AddObserver(this);
    }
    public void UpdateObserver(int data)
    {
        scoreText.text = "Score: "+data;
    }


}
