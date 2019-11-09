using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineGunController : MonoBehaviour {
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    private float lastShot = 0;

    public Transform firePoint;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (isFiring) {
            //if (Time.time > timeBetweenShots + lastShot) {
            //    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //    newBullet.speed = bulletSpeed;
            //    lastShot = Time.time;
            //}
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            Vector3 targetPos = new Vector3();
            if (plane.Raycast(ray, out distance))
            {

                // Target value is the instant location of cursor, can be used for shooting function later
                targetPos = ray.GetPoint(distance);
            }

            AbilitiesDirectory.TryCastAbility("shotgunAttack", GetComponent<Entity>(), targetPos);
        }
    }
}
