using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

/// <summary>
/// Base class for the other bullet types
/// </summary>
public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    public int damageToGive;
    public GameObject impactEffect;
    public AudioClip collisionSound;
    public Vector3 shootingLoc;
    public Vector3 targetLoc;

    public Entity entity;

    // Update is called once per frame
    protected virtual void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(obj, 2f);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the bullets with a starting location and an end location
    /// </summary>
    /// <param name="shootingLoc"></param>
    /// <param name="targetLoc"></param>
    public void InitBullet(Vector3 shootingLoc, Vector3 targetLoc)
    {
        Debug.Log("Initializing bullet: " + shootingLoc + ", " + targetLoc);
        this.shootingLoc = shootingLoc;
        this.targetLoc = targetLoc;
    }

    /// <summary>
    /// Collision Enter function
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnCollisionEnter(Collision other)
    {
        SoundManager.PlaySoundatLocation(collisionSound, other.transform.position);

        if (gameObject.tag == "Bullet")
        {
            //if (other.gameObject.tag == "Enemy")
            //{
            //    other.gameObject.GetComponent<Enemy>().HurtEnemy(damageToGive);
            //    GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            //    Destroy(obj, 2f);
            //    Destroy(gameObject);
            //}

            // For now anything with this tag can be destroyed in the same way.
            // For now anything with this tag can be destroyed in the same way.
            if (other.gameObject.tag == "Destructible" || other.gameObject.GetComponent<Teams>().TeamsFaction != entity.team)
            {
                other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(obj, 2f);
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<Entity>() == entity) // If it's the caster case
            {
                return;
            }

            // Shooting team mate case
            if (other.gameObject.GetComponent<Teams>().TeamsFaction == entity.team)
            {
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            // For nexus
/*            if (other.gameObject.tag == "Nexus")
            {
                other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(obj, 2f);
                Destroy(gameObject);
            }*/
        }
    }
}
