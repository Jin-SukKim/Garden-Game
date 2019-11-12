using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineGunController : MonoBehaviour {
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    private float lastShot = 0;

    public Transform firePoint;
    public Transform targeting;

    private int fireMode;
    public int FireMode {
        get { return fireMode; }
        set { 
            fireMode = value;
            if(fireMode >= Enum.GetNames(typeof(BulletController.BulletType)).Length)
            {
                fireMode = 0;
            }
        }
    }

    private LineRenderer laserSight;

    public int spineCount;

    // Start is called before the first frame update
    void Start() {
        fireMode = 0;
        laserSight = gameObject.GetComponent<LineRenderer>();
        laserSight.useWorldSpace = false;
        laserSight.positionCount = 2;
    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            laserSight.SetPosition(1, new Vector3(0,0,(float)(hit.distance / 0.3514)));
        }
        else
        {
            laserSight.SetPosition(1, new Vector3(0,0,(float) (20/ 0.3514)));
        }

        if (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                // Target value is the instant location of cursor, can be used for shooting function later
                Vector3 target = ray.GetPoint(distance);

                if(Vector3.Distance(transform.position, target) < 7.5f)
                {
                    targeting.position = target + new Vector3(0, 0.1f);
                }
            }
        }

        if (Input.GetKeyDown("q"))
        {
            FireMode++;
            transform.Find("Targeting").gameObject.SetActive(FireMode == (int)BulletController.BulletType.arc);
        }

        if (isFiring)
        {
            if (Time.time > timeBetweenShots + lastShot)
            {
                switch (fireMode)
                {
                    case 0:
                        fireStraight();
                        break;
                    case 1:
                        fireArc();
                        break;
                    case 2:
                        if(spineCount == 0)
                            fireSpine();
                        break;
                    default:
                        fireStraight();
                        break;
                }
            }
        }
    }

    public void fireStraight()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.type = BulletController.BulletType.straight;
        newBullet.speed = bulletSpeed;
        lastShot = Time.time;
    }

    public void fireArc()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.type = BulletController.BulletType.arc;
        newBullet.startPos = firePoint.position;

        newBullet.endPos = targeting.position;
        lastShot = Time.time;
    }

    public void fireSpine()
    {
        Debug.Log("Spine: " + transform.position);
        StartCoroutine(spineShoot(transform.position, transform.forward));
    }

    IEnumerator spineShoot(Vector3 startPos, Vector3 startDir)
    {
        while (spineCount < 5)
        {
            BulletController newBullet = Instantiate(bullet, startPos, firePoint.rotation) as BulletController;
            newBullet.type = BulletController.BulletType.spine;
            newBullet.owner = this;
            newBullet.startPos = startPos;
            newBullet.startDir = startDir;

            spineCount++;
            lastShot = Time.time;
            yield return new WaitForSeconds(0.2f);
        }
        spineCount = 0;

    }
}
