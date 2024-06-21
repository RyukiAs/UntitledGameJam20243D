using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;

    public float timeTilEnemySpawn = 2f;
    private float trackTimeUntilSpawn = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trackTimeUntilSpawn += Time.deltaTime;
        if(trackTimeUntilSpawn >= timeTilEnemySpawn)
        {
            Vector3 position = new Vector3(Player.transform.position.x + , Player.transform.position.y + 10f, Player.transform.position.z);
            Instantiate(enemy, position, Quaternion.identity);
            trackTimeUntilSpawn = 0;
        }
    }
}
