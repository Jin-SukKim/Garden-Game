using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class BulletController : MonoBehaviour
{
    public enum BulletType { straight, arc, spine };
    public float speed;
    public float lifeTime;

    public int damageToGive;
    public GameObject impactEffect;
    public BulletType type;

    public Vector3 shootingLoc;
    public Vector3 targetLoc;

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
    private void OnCollisionEnter(Collision other)
    {
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
            if (other.gameObject.tag == "Destructible")
            {
                other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                Destroy(obj, 2f);
                Destroy(gameObject);
            }
        }
    }
}
