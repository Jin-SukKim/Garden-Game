using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public float health;

    private float currentHealth;
    public bool respawn = false;

    public GameObject enemeyDeathAnimation;
    public GameObject respawnPoint;
    public GameObject respawnPointWait;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    /// <summary>
    /// Determines if the object is still alive and destroys it if health is lower than 0
    /// </summary>
    void Update()
    {
        if (currentHealth <= 0 && respawn == false)
        {
            GameObject obj = (GameObject)Instantiate(enemeyDeathAnimation, transform.position, Quaternion.identity);
            //Destroy(gameObject.GetComponent("Rigidbody"));
            Destroy(obj, 5f);
            Destroy(gameObject);
        }
        else if (currentHealth <= 0 && respawn == true)
        {
            gameObject.transform.position = respawnPointWait.transform.position;
            StartCoroutine(waitingRespawn());
        }
    }

    /// <summary>
    /// Decreases health amount by damage value parameter
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    /// <summary>
    /// Respawn function that moves the object to the respawn point and resets health
    /// </summary>
    public void Respawn()
    {
        if (respawn)
        {
            gameObject.transform.position = respawnPoint.transform.position;
            currentHealth = health;
        }
    }

    /// <summary>
    /// Waiting function for respawn that waits 3 seconds before respawning the object
    /// </summary>
    /// <returns></returns>
    IEnumerator waitingRespawn()
    {
        yield return new WaitForSeconds(3);
        Respawn();
    }
}
