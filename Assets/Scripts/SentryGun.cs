using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryGun : MonoBehaviour
{
    public bool isFiring;

    public SentryController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    private float lastShot = 0;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isFiring)
        {
            if (Time.time > timeBetweenShots + lastShot)
            {
                SentryController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as SentryController;
                newBullet.speed = bulletSpeed;
                lastShot = Time.time;
            }
        }
    }
}
