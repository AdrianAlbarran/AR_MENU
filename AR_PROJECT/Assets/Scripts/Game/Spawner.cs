using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [HideInInspector]
    private GameObject[] array;
    public GameObject[] spawnPointsArray;
    public Vector3 izq;
    public Vector3 derecha;
    private Vector3 fatherPos;
    public Vector3 offset;

    private bool onCD = true;

    public float CD;

    public void Start()
    {    
        fatherPos = transform.parent.position;
    }

    private void OnEnable()
    {
        array = FindObjectOfType<RaycastARScriptGame>().ingredientsArray;
        onCD = false;
    }

    public void Update()
    {
        if (onCD) return;
        Spawn();
    }

    public void Spawn()
    {
        StartCoroutine(CDspawn());
        //float dispacementX = UnityEngine.Random.Range(izq.x, derecha.x);
        //float xPos = fatherPos.x + dispacementX;


        //float offAngle = Vector3.Angle(this.transform.right, new Vector3(1, 0, 0));

        //float newX = Mathf.Cos(offAngle * Mathf.Deg2Rad) * xPos;
        //float newZ = Mathf.Sin(offAngle * Mathf.Deg2Rad) * dispacementX;

        int indice = UnityEngine.Random.Range(0, array.Length);

        int randomPos = UnityEngine.Random.Range(0, spawnPointsArray.Length);
   
        Vector3 spawnPoint = new Vector3(spawnPointsArray[randomPos].transform.position.x, spawnPointsArray[randomPos].transform.position.y + offset.y, spawnPointsArray[randomPos].transform.position.z);
        GameObject aux = Instantiate(array[indice], spawnPoint, Quaternion.identity);

    }
    IEnumerator CDspawn()
    {
        onCD = true;
        yield return new WaitForSeconds(CD);
        onCD = false;
    }
}
