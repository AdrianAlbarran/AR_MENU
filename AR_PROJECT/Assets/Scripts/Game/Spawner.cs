using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        float dispacementX = UnityEngine.Random.Range(izq.x, derecha.x);
        float xPos = fatherPos.x + dispacementX;
        int indice = UnityEngine.Random.Range(0, array.Length);

        float offAngle = Vector3.Angle(this.transform.right, new Vector3(1, 0, 0));

        UnityEngine.Debug.Log(offAngle);

        float newX = Mathf.Cos(offAngle * Mathf.Deg2Rad) * xPos;
        float newZ = Mathf.Sin(offAngle * Mathf.Deg2Rad) * dispacementX;

        UnityEngine.Debug.Log("newz " + newZ);
        UnityEngine.Debug.Log("fatherPosZ" + fatherPos.z);
        GameObject aux = Instantiate(array[indice], new Vector3(newX, fatherPos.y + 1, newZ + fatherPos.z), Quaternion.identity);

        UnityEngine.Debug.Log("aux.z" + aux.transform.position.z);
    }
    IEnumerator CDspawn()
    {
        onCD = true;
        yield return new WaitForSeconds(CD);
        onCD = false;
    }
}
