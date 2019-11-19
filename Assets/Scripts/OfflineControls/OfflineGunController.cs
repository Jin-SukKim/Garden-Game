using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OfflineGunController : MonoBehaviour {
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;

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
    private int maxSpines = 5;

    // Start is called before the first frame update
    void Start() {
        fireMode = 0;
        laserSight = gameObject.GetComponent<LineRenderer>();
        laserSight.useWorldSpace = false;
        laserSight.positionCount = 2;
    }


    //Laser sight
    public void RenderLaserBeam()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(hit.distance / 0.3514)));
        }
        else
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(20 / 0.3514)));
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

                if (Vector3.Distance(transform.position, target) < 7.5f)
                {
                    targeting.position = target + new Vector3(0, 0.1f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {

        RenderLaserBeam();

        if (isFiring) {
            //if (Time.time > timeBetweenShots + lastShot) {
            //    BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            //    newBullet.speed = bulletSpeed;
            //    lastShot = Time.time;
            //}

            

            if (Time.time > timeBetweenShots + lastShot)
            {
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
                /*
                switch (fireMode)
                {
                    case 0:
                        //FireStraight();
                        break;
                    case 1:
                        AbilitiesDirectory.TryCastAbility("shotgunAttack", GetComponent<Entity>(), targetPos);
                        break;
                    case 2:
                        //Prevents spines from being shot while this attack is already active
                        //if (spineCount == 0)
                            //FireSpine();
                        break;
                    case 3:
                        //FireArc();
                        AbilitiesDirectory.TryCastAbility("LobAttack", GetComponent<Entity>(), targetPos);
                        break;
                    default:
                        //FireStraight();
                        break;
                }
                */
            }

            
        }

        if (Input.GetKeyDown("q"))
        {
            if(spineCount == 0)
            {
                FireMode++;
                transform.Find("Targeting").gameObject.SetActive(FireMode == (int)BulletController.BulletType.arc);
            }
        }

        /*
        if (isFiring)
        {
            if (Time.time > timeBetweenShots + lastShot)
            {
                switch (fireMode)
                {
                    case 0:
                        FireStraight();
                        break;
                    case 1:
                        FireArc();
                        break;
                    case 2:
                        //Prevents spines from being shot while this attack is already active
                        if(spineCount == 0)
                            FireSpine();
                        break;
                    default:
                        FireStraight();
                        break;
                }
            }
        }
        */
    }

    /*
    /// <summary>
    /// Fires the straight shot
    /// </summary>
    public void FireStraight()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.type = BulletController.BulletType.straight;
        newBullet.speed = bulletSpeed;
        lastShot = Time.time;
    }

    /// <summary>
    /// Fires the arc shot
    /// </summary>
    public void FireArc()
    {
        BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
        newBullet.type = BulletController.BulletType.arc;
        newBullet.startPos = firePoint.position;

        newBullet.endPos = targeting.position;
        lastShot = Time.time;
    }

    /// <summary>
    /// Starts the coroutine to shoot the spine attack
    /// </summary>
    public void FireSpine()
    {
        StartCoroutine(spineShoot(transform.position, transform.forward));
    }

    IEnumerator spineShoot(Vector3 startPos, Vector3 startDir)
    {
        while (spineCount < maxSpines)
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
    */
}
