using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{

    private float initialDistance;
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);
            
            if(touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled || touchOne.phase == TouchPhase.Canceled)
            {
                return;
            }

            if(touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                initialScale = transform.localScale;
            }
            else
            {
                float currentDistance = Vector2.Distance(touchZero.position,touchOne.position);

                if (Mathf.Approximately(initialDistance, 0))
                {
                    return;
                }

                float factor = currentDistance / initialDistance;
                transform.localScale =initialScale*factor;

            }

        }
    }
}
