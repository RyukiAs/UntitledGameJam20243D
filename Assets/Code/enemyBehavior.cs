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
    void Update()
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

        if (theirGameObject.GetComponent<PlayerBehavior>() != null)
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
            if (theirHealthSystem != null)
            {
                theirHealthSystem.TakeDamage(1);
            }

        }

    }

}
