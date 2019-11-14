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
            StartCoroutine(WaitingRespawn());
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
    }

    public void Respawn()
    {
        if (respawn)
        {
            gameObject.transform.position = respawnPoint.transform.position;
            currentHealth = health;
        }
    }

    IEnumerator WaitingRespawn()
    {
        yield return new WaitForSeconds(3);
        Respawn();
    }
}
