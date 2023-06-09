
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubject<int>
{
    private float horizontal;
    private Vector3 pos;
    public Camera cameraSceneAR;
    private Vector3 initialPhonePosition;
    private bool isMoving = false;
    private Vector3 vecToCamera;
    private Rigidbody _rb;

    public int score;
    void Start()
    {
        cameraSceneAR = Camera.main;
        pos = transform.position;
        _rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        Vector3 look = cameraSceneAR.transform.forward;
        look.Normalize();
        vecToCamera = this.pos - cameraSceneAR.transform.position;
        vecToCamera.Normalize();
        Debug.Log(vecToCamera);

        // angle in [0,180]
        float angle = Vector3.Angle(look, vecToCamera);
        float sign = Mathf.Sign(Vector3.Dot(look, Vector3.Cross(new Vector3(0, 1, 0), vecToCamera)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        Debug.Log("angulo: " + angle);
        Debug.Log("angulo con signo: " + signed_angle);

        angle = Mathf.Deg2Rad * signed_angle;
        float dis = Mathf.Sin(angle) * 0.1f;

        Debug.Log(dis);

        _rb.velocity = new Vector3(-this.transform.forward.x * dis, 0, 0); 
        
    }

    private void FixedUpdate()
    {
        //transform.position = new Vector3(camera.transform.position.x, transform.position.y, transform.position.z);
    }

    public void UpdateScore(int value)
    {
        score += value;
        NotifyObservers();
    }

    private List<IObserver<int>> _observers = new List<IObserver<int>>();
    public void AddObserver(IObserver<int> observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver<int> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver<int> observer in _observers)
        {
            observer?.UpdateObserver(score);
        }
    }
}
