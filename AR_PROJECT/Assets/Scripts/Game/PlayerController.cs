
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubject<int>
{
    private float horizontal;
    private Vector3 pos;
    public Camera cameraSceneAR;
    private Vector3 vecToCamera;
    private Rigidbody _rb;
    private float dis;

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
        
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        Vector3 look = cameraSceneAR.transform.forward;
        look.Normalize();
        vecToCamera = this.pos - cameraSceneAR.transform.position;
        vecToCamera.Normalize();

        // angle in [0,180]
        float angle = Vector3.Angle(new Vector2(look.x, look.z), new Vector2(vecToCamera.x, vecToCamera.z));
        float sign = Mathf.Sign(Vector3.Dot(look, Vector3.Cross(new Vector3(0, 1, 0), vecToCamera)));

        // angle in [-179,180]
        float signed_angle = angle * sign;


        if (signed_angle > -5 && signed_angle < 5)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            _rb.AddForce(transform.right * dis, ForceMode.VelocityChange);

            signed_angle = Mathf.Deg2Rad * signed_angle;
            dis = Mathf.Sin(signed_angle);
            dis = dis >= 0.35f ? dis * .2f : dis * .1f;

        }
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
