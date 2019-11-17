using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeTIme;

    public int damageToGive;
    public GameObject impactEffect;
    public Entity entity;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lifeTIme -= Time.deltaTime;

        if (lifeTIme <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Determines if the object loses health on collision with the bullet object
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "Bullet")
        {
            // For now anything with this tag can be destroyed in the same way.
            if (other.gameObject.tag == "Destructible" || other.gameObject.GetComponent<Teams>().TeamsFaction != entity.team)
            {
                other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(obj, 2f);
                Destroy(gameObject);
            } else if (other.gameObject.GetComponent<Entity>() == entity) // If it's the caster case
            {
                return;
            }

            // Shooting team mate case
            if (other.gameObject.GetComponent<Teams>().TeamsFaction == entity.team)
            {
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
