
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubject<int>
{
    private float horizontal;
    private Vector3 pos;
    public Camera cameraSceneAR;

    public int score;
    void Start()
    {
        cameraSceneAR = Camera.main;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
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
