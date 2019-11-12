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

    private float arcLifetime;

    public int damageToGive;
    public GameObject impactEffect;
    public BulletType type;

    float animationStart;
    Vector3 midPoint;

    public Vector3 endPos;
    public Vector3 startPos;
    public Vector3 startDir;

    public OfflineGunController owner;

    // Start is called before the first frame update
    void Start()
    {
        if(type == BulletType.arc)
        {
            arcLifetime = 2f;
            lifeTime = arcLifetime;
            animationStart = Time.time;
            //The apex of the arc
            midPoint = ((endPos + startPos) / 2) + new Vector3(0, 10f);
        }else if(type == BulletType.spine)
        {
            transform.position = startPos;
            transform.position += Vector3.down;
            transform.position += owner.spineCount * startDir;
            lifeTime = 0.25f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(type == BulletType.arc)
        {
            float lerpVal = (Time.time - animationStart) / arcLifetime;

            Vector3 curve1 = Vector3.Lerp(startPos, midPoint, lerpVal);
            Vector3 curve2 = Vector3.Lerp(midPoint, endPos, lerpVal);
            transform.position = Vector3.Lerp(curve1, curve2, lerpVal);
        }
        else if(type == BulletType.spine)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if(type == BulletType.straight)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            GameObject obj = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(obj, 2f);
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

    void OnDestroy()
    {
        //Debug.Log("Bullet Destroyed");
    }
}
