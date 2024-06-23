using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    enum BehaviorState
    {
        Fall,
        Chase,
        Still,
        Wander,
    }

    public float speed;

    private BehaviorState state;
    private Vector3 wanderVelocity;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity += Vector3.down * 0.02f;
    }

    void switchBehaviorState() {
        float seed = Random.Range(0.0f, 1.0f);

        if (References.thePlayer == null) return;

        if ((References.thePlayer.transform.position - transform.position).magnitude < 5.0 && seed < 0.5) {
            state = BehaviorState.Chase;
            return;
        }

        if (seed < 0.8) {
            state = BehaviorState.Wander;
            return;
        }

        state = BehaviorState.Still;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        switch (state) { 
            case BehaviorState.Fall:
                if (rb.velocity.y >= -0.01) switchBehaviorState();
                break;
            case BehaviorState.Chase:
                if (References.thePlayer != null)
                {
                    Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position;
                    rb.velocity = vectorToPlayer.normalized * speed;
                }
                break;
            case BehaviorState.Wander:
                if (Random.Range(0.0f, 1.0f) < 0.01)
                {
                    wanderVelocity = new Vector3(Random.Range(-1.0f, 1.0f), rb.velocity.y, Random.Range(-1.0f, 1.0f));
                }
                rb.velocity = wanderVelocity;
                break;
            case BehaviorState.Still:
            default:
                break;
        }

        if (state != BehaviorState.Fall && Random.Range(0.0f, 1.0f) < 0.01f) switchBehaviorState();
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        GameObject theirGameObject = thisCollision.gameObject;
        PlayerBehavior player = theirGameObject.GetComponent<PlayerBehavior>();

        if (player != null)
        {
            if (player.attacking)
            {
                HealthSystem enemyHealth = gameObject.GetComponent<HealthSystem>();
                enemyHealth.TakeDamage(1);
                player.playerSize += 1;
                player.changeSize();


                AudioSource chomp = player.GetComponent<AudioSource>();
                chomp.Play();
            }
            else
            {
                HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();
                if(player.transform.localScale.x <= transform.localScale.x) // do 1 dmg if player is smaller
                {
                    if (theirHealthSystem != null)
                    {
                        theirHealthSystem.TakeDamage(1);
                    }
                }else if (transform.localScale.x < player.transform.localScale.x) //do thisGameobjectscale/playerscale damage ex(0.1/1)
                {
                    float damage = transform.localScale.x / player.transform.localScale.x;
                    theirHealthSystem.TakeDamage(damage);
                }
                
            }

        }

    }

}
