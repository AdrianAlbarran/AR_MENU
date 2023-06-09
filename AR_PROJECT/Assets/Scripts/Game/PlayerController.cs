using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ISubject<int>
{
    private float horizontal;
    private Vector3 pos;
    public Camera cameraSceneAR;
    private Vector3 vecToCamera;
    private Rigidbody _rb;
    private float dis;
    private float prevCameraZ;
    private float actualCameraZ;
    [SerializeField] private Transform colliderZP;
    [SerializeField] private Transform colliderZN;

    private bool moveUp;
    private bool moveDown;
    public int score;
    [SerializeField] private float speed;
    private float verticalMovement;
    private float lastVerticalMovement;

    private void Start()
    {
        lastVerticalMovement = 0;
        cameraSceneAR = Camera.main;
        pos = transform.position;
        _rb = GetComponent<Rigidbody>();
        //prevCameraZ = cameraSceneAR.transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        pos = transform.position;
        Vector3 look = cameraSceneAR.transform.forward;
        look.Normalize();
        vecToCamera = pos - cameraSceneAR.transform.position;
        vecToCamera.Normalize();

        // angle in [0,180]
        float angle = Vector3.Angle(new Vector2(look.x, look.z), new Vector2(vecToCamera.x, vecToCamera.z));
        float sign = Mathf.Sign(Vector3.Dot(look, Vector3.Cross(new Vector3(0, 1, 0), vecToCamera)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        // Z movement
        //actualCameraZ = cameraSceneAR.transform.position.z;
        //float deltaZ = (actualCameraZ - prevCameraZ) * 2;

        //mov eje z
        //transform.position = new Vector3(transform.position.x, transform.position.y, CheckOutOfBounds(transform.position.z + deltaZ));
        if (signed_angle > -3 && signed_angle < 3)
        {
            _rb.velocity = Vector3.zero;
        }
        else
        {
            // fuerza para el eje xz
            signed_angle = Mathf.Deg2Rad * signed_angle;
            dis = Mathf.Sin(signed_angle);
            dis = dis >= 0.20f ? dis * .3f : dis * .2f;
            _rb.AddForce(transform.right * dis, ForceMode.VelocityChange);

            //_rb.AddForce(transform.right * dis, ForceMode.VelocityChange);

            //signed_angle = Mathf.Deg2Rad * signed_angle;
            //dis = Mathf.Sin(signed_angle);
            //dis = dis >= 0.35f ? dis * .2f : dis * .1f;
        }
        //prevCameraZ = cameraSceneAR.transform.position.z;
        if (verticalMovement!=0)
        {
            Debug.Log(transform.forward);
            //_rb.AddForce(transform.forward*verticalMovement, ForceMode.Force);
            _rb.velocity = transform.forward * verticalMovement;

            lastVerticalMovement = verticalMovement;
        }

    }

    private float CheckOutOfBounds(float zPos)
    {
        return Mathf.Clamp(zPos, colliderZN.position.z, colliderZP.position.z);
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

    public void UpButtonPressed()
    {
        moveUp = true;
    }

    public void UpButtonReleased()
    {
        moveUp = false;
    }

    public void DownButtonPressed()
    {
        moveDown = true;
    }

    public void DownButtonReleased()
    {
        moveDown = false;
    }

    private void Movement()
    {
        if (moveUp)
        {
            verticalMovement = speed;
        }
        else if (moveDown)
        {
            verticalMovement = -speed;
        }
        else
        {
            verticalMovement = 0;
        }
    }
}