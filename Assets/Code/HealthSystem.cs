using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    [FormerlySerializedAs("health")] //write this to not lose data when renaming
    public float maxHealth;
    float currentHealth;

    public GameObject healthBarPrefab;

    //public GameObject deathEffectPrefab;

    HealthBar myHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //Create health panel ON the canvas
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBar>();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        //transform.localScale *= 0.5f;

        if (currentHealth <= 0)
        {
            /*
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            }
            */
            Destroy(gameObject);

        }
    }

    private void OnDestroy()
    {
        //don't create anything in the ondestroy event - only for cleaning
        if (myHealthBar != null)
        {
            Destroy(myHealthBar.gameObject);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //make our healthbar reflect our health - myHealthBar.ShowHealth
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);
        //Make our healthbar follow us - move to our current position
        //myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.forward*0.2f);


    }
}
