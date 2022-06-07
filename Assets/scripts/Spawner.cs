using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnTime = 5f;
    public float spawnDelay = 3f;
    public GameObject[] enemies;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnDelay, spawnTime);
    }
    void SpawnEnemy()
    {
        int index = Random.Range(0, enemies.Length);
        Instantiate(enemies[index], transform.position, transform.localRotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
