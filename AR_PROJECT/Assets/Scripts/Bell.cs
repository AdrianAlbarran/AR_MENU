using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bell : MonoBehaviour
{
    SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        StartCoroutine(ActivateCollider());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ActivateCollider()
    {

        yield return new WaitForSeconds(.6f);
        sphereCollider.enabled = true;
    }
}
