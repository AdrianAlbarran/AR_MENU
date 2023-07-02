using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drops : MonoBehaviour
{
    private Rigidbody _rb;

    public int points;
    public float speed;

    public bool isBomb;

    public GameObject prefab;

    private GameObject prefabGO;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.useGravity = false;

        _rb.AddForce(new Vector3(0, -speed, 0));

        RaycastHit rayHit;

        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out rayHit, Mathf.Infinity))
        {
            prefabGO = Instantiate(prefab, rayHit.point + new Vector3(0, 0.0001f, 0), Quaternion.identity);
            prefabGO.transform.Rotate(new Vector3(270, 0, 0));
        }
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
            if (isBomb)
            {
                FindObjectOfType<RaycastARScriptGame>().EndGame();
            }
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(prefabGO);
    }
}