using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (References.thePlayer != null)
        {
            Rigidbody ourRigidBoyd = GetComponent<Rigidbody>();
            Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position;
            ourRigidBoyd.velocity = vectorToPlayer.normalized * speed;
        }


    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        PlayerBehavior player = theirGameObject.GetComponent<PlayerBehavior>();

        if (player != null)
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
            if (player.attacking)
            {
                HealthSystem enemyHealth = gameObject.GetComponent<HealthSystem>();
                enemyHealth.TakeDamage(1);
            }
            else
            {
                if (theirHealthSystem != null)
                {
                    theirHealthSystem.TakeDamage(1);
                }
            }

        }

    }

}
