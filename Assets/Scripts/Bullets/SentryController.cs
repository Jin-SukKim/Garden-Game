﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryController : MonoBehaviour
{
    public float speed;
    public float lifeTIme;

    public int damageToGive;
    public GameObject impactEffect;


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