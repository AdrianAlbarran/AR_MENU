using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] array;

    public Vector3 izq;
    public Vector3 derecha;
    private Vector3 fatherPos;
    public Vector3 offset;

    private bool onCD;

    public float CD;

    public void Start()
    {
        fatherPos = transform.parent.position;
    }


    public void Update()
    {
        if (onCD) return;
        Spawn();
    }

    public void Spawn()
    {
        StartCoroutine(CDspawn());
        //float pos = fatherPos.x + Random.Range(izq.x,derecha.x);
        int indice = UnityEngine.Random.Range(0, array.Length);
        Debug.Log(fatherPos);
        GameObject aux = Instantiate(array[indice], new Vector3(fatherPos.x, fatherPos.y + 1, fatherPos.z), Quaternion.identity);

    }
    IEnumerator CDspawn()
    {
        onCD = true;
        yield return new WaitForSeconds(CD);
        onCD = false;
    }
}
