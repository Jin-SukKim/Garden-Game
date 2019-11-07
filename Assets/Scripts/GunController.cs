using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class GunController : MonoBehaviourPun
{
    public bool isFiring;

    public BulletController bullet;
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
        shoot();

    }
    public void shoot()
    {
        if (isFiring)
        {
            if (Time.time > timeBetweenShots + lastShot)
            {
                this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint.position, firePoint.rotation);
            }
        }
    }
    [PunRPC]
    private void Fire(Vector3 pos, Quaternion rot)
    {   
        BulletController newBullet = Instantiate(bullet, pos, rot) as BulletController;
        newBullet.speed = bulletSpeed;
        lastShot = Time.time;
        newBullet.team = this.transform.parent.GetComponent<Teams>().TeamsFaction;
    }


}
