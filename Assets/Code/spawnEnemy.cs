using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{
    public GameObject Player;
    public GameObject enemy;
    public Transform arena;

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
            if(Player != null)
            {
                Vector3 position = new Vector3(Player.transform.position.x + UnityEngine.Random.Range(-6f, 6f), Player.transform.position.y + 10f, +UnityEngine.Random.Range(-6f, 6f));
                GameObject enemyPrefab = Instantiate(enemy, arena);
                if(Player.transform.localScale.x >= 3)
                {

                }else if(Player.transform.localScale.x >= 2)
                {

                }else if(Player.transform.localScale.x >= 1)
                {

                }


                //GameObject enemy = Instantiate(enemy, position, Quaternion.identity);
                trackTimeUntilSpawn = 0;
                enemyPrefab.transform.position = position;
            }
            
        }
    }
}
