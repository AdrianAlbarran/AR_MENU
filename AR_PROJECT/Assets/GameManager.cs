using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Spawner spawner;

    [SerializeField] private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        spawner.enabled = true;
    }

    public void EndGame()
    {
        spawner.enabled = false;
    }
    public int GetPoints()
    {
        return pc.score;
    }
}
