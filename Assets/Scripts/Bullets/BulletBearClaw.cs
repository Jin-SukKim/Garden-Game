using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBearClaw : BulletController
{
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.Translate(Vector3.forward * 400);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (gameObject.tag == "Bullet")
        {
            if (other.gameObject.tag == "Destructible" || other.gameObject.GetComponent<Teams>().TeamsFaction != entity.team)
            {
                other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
                GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
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
        }
    }
}
