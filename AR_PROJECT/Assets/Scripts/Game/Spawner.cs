using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] array;

    public Vector3 izq;
    public Vector3 derecha;

    public Vector3 offset;

    private bool onCD;

    public float CD;

    public void Start()
    {
        
    }


    public void Update()
    {
        if (onCD) return;
        Spawn();
    }

    public void Spawn()
    {
        StartCoroutine(CDspawn());
        float pos = Random.Range(izq.x,derecha.x);
        int indice = Random.Range(0, array.Length);
        Instantiate(array[indice],new Vector3(pos,offset.y,offset.z), Quaternion.identity);
    }

    IEnumerator CDspawn()
    {
        onCD = true;
        yield return new WaitForSeconds(CD);
        onCD = false;
    }
}
