using UnityEngine;
using System.Collections;

public class Bulldozer : BulletController
{

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Destructible" || other.gameObject.GetComponent<Teams>().TeamsFaction != entity.team)
        {
            other.gameObject.GetComponent<DamageSystem>().Damage(damageToGive);
            GameObject obj = (GameObject)Instantiate(impactEffect, transform.position + transform.forward, transform.rotation);
            Destroy(obj, 1f);
        }
    }
}
