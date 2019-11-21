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
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) && hit.collider.gameObject.tag != "Bullet")
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(hit.distance / 0.3514)));
        }
        else
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(20 / 0.3514)));
        }
    }

    // Update is called once per frame
    void Update() {

        RenderLaserBeam();

        if (isFiring) {

            if (Time.time > timeBetweenShots + lastShot)
            {
                if(fireMode == 0)
                {
                    RaycastHit hit;
                    Vector3 targetPos = transform.position + (10 * transform.forward);
                    AbilitiesDirectory.TryCastAbility("shotgunAttack", GetComponent<Entity>(), targetPos);
                }
            }

            
        }

        if (Input.GetMouseButtonUp(1))
        {
            transform.Find("Targeting").gameObject.SetActive(false);
            Debug.Log("Fire: " + targeting.position);
            AbilitiesDirectory.TryCastAbility("lobAttack", GetComponent<Entity>(), targeting.position);
        }

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                Vector3 target = ray.GetPoint(distance);

                if (Vector3.Distance(transform.position, target) < 7.5f)
                {
                    targeting.position = target + new Vector3(0, 0.1f);
                }
            }
            transform.Find("Targeting").gameObject.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Q)){
            AbilitiesDirectory.TryCastAbility("shootAttack", GetComponent<Entity>());
        }

        if (Input.GetKey(KeyCode.E))
        {
            AbilitiesDirectory.TryCastAbility("spineAttack", GetComponent<Entity>());
        }

    }

}
