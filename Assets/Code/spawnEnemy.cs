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
                /*
                if(Player.transform.localScale.x >= 1.2f)
                {
                    enemyPrefab.transform.localScale = new Vector3(10f,10f,10f);
                    HealthSystem health = enemyPrefab.GetComponent<HealthSystem>();
                    health.maxHealth = 5;
                    health.currentHealth = 5;
                }
                else if(Player.transform.localScale.x >= 0.8f)
                {
                    enemyPrefab.transform.localScale = new Vector3(6f, 6f, 6f);
                    HealthSystem health = enemyPrefab.GetComponent<HealthSystem>();
                    health.maxHealth = 3;
                    health.currentHealth = 3;
                }
                else if(Player.transform.localScale.x >= 0.4f)
                {
                    enemyPrefab.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    HealthSystem health = enemyPrefab.GetComponent<HealthSystem>();
                    health.maxHealth = 2;
                    health.currentHealth = 2;
                }
                */
                float randomX = UnityEngine.Random.Range(0, 1f);
                float randomY = UnityEngine.Random.Range(0, 1f);
                float randomZ = UnityEngine.Random.Range(0, 1f);
                enemyPrefab.transform.localScale = new Vector3(Player.transform.localScale.x + randomX, Player.transform.localScale.y + randomY, Player.transform.localScale.z + randomZ);
                HealthSystem health = enemyPrefab.GetComponent<HealthSystem>();
                //float setHp = UnityEngine.Random.Range(0, 100);
                float averageGain = (randomX + randomY + randomZ) / 3f; //max = 1

                if(averageGain < 0.5f)
                {
                    health.maxHealth = 1;
                    health.currentHealth = 1;
                }else if (averageGain < 0.8f)
                {
                    health.maxHealth = 2;
                    health.currentHealth = 2;
                }else
                {
                    health.maxHealth = 3;
                    health.currentHealth = 3;
                }
                /*
                if (setHp < 50)
                {
                    health.maxHealth = 1;
                    health.currentHealth = 1;
                }else if(setHp < 80)
                {
                    health.maxHealth = 2;
                    health.currentHealth = 2;
                }
                else
                {
                    health.maxHealth = 3;
                    health.currentHealth = 3;
                }
                */

                //GameObject enemy = Instantiate(enemy, position, Quaternion.identity);
                trackTimeUntilSpawn = 0;
                enemyPrefab.transform.position = position;
            }
            
        }
    }
}
