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

    // Hacky?
    private Vector3 firePoint;
    private Abilities abilities;

    public PhotonView photonView;




    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        abilities = GetComponent<Abilities>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PHOTON VIEW GUN: " + photonView);
        Debug.Log("UPDATING GUN");
        if (photonView.IsMine || !PhotonNetwork.IsConnected)
        {

            //// Points the player's cube object in the direction of the cursor
            //// Ray casting to get the cursor position, uses that result to direct player
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                // Target value is the instant location of cursor, can be used for shooting function later
                firePoint = ray.GetPoint(distance);

                Vector3 direction = firePoint - transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            //shooting mechanics Will update to INPUTMAN later
            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isFiring = false;
            }

            if (isFiring)
            {
                if (Time.time > timeBetweenShots + lastShot)
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint);
                    }

                }
            }
        }
    }

    [PunRPC]
    private void Fire(Vector3 pos)
    {
        // OLD
        /*BulletController newBullet = Instantiate(bullet, pos, rot) as BulletController;
        newBullet.speed = bulletSpeed;
        lastShot = Time.time;
        newBullet.team = this.transform.parent.GetComponent<Teams>().TeamsFaction;
        lastShot = Time.time;*/

        // NEW
        abilities.basicAttack(pos);
        //newBullet.team = this.transform.parent.GetComponent<Teams>().TeamsFaction;
    }


}
