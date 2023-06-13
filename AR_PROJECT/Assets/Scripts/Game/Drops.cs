using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drops : MonoBehaviour
{
    private Rigidbody _rb;

    public int points;
    public float speed;

    private void Start()
    {

        _rb = GetComponent<Rigidbody>();

        _rb.useGravity = false;

        _rb.AddForce(new Vector3(0, -speed, 0));
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<PlayerController>().UpdateScore(points);
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
    }
}