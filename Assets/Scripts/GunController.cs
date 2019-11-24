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
    //private float shotCounter;

    private float lastShot = 0;

    // Hacky?
    private Vector3 firePoint;
    private Abilities abilities;

    public PhotonView photonView;

    // Adam's
    public Transform targeting;
    private LineRenderer laserSight;



    // Start is called before the first frame update
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        abilities = gameObject.GetComponent<Abilities>();
        
        // Just for testin
/*        laserSight = gameObject.GetComponent<LineRenderer>();
        laserSight.useWorldSpace = false;
        laserSight.positionCount = 2;*/
    }

    //Laser sight
    public void RenderLaserBeam()
    {
       /* RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) && hit.collider.gameObject.tag != "Bullet")
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(hit.distance / 0.3514)));
        }
        else
        {
            laserSight.SetPosition(1, new Vector3(0, 0, (float)(20 / 0.3514)));
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("PHOTON VIEW GUN: " + photonView);
        Debug.Log("UPDATING GUN");

        RenderLaserBeam();

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
                        Debug.Log("NOT CONNECtED TO NEt");
                        /*RaycastHit hit;
                        Vector3 targetPos = transform.position + (10 * transform.forward);
                        AbilitiesDirectory.TryCastAbility("shotgunAttack", GetComponent<Entity>(), targetPos);*/
                        Fire(firePoint);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint);
                    }
                }
            }



            //Fire Lob
/*            if (Input.GetMouseButtonUp(1))
            {
                transform.Find("Targeting").gameObject.SetActive(false);
                Debug.Log("Fire: " + targeting.position);
                AbilitiesDirectory.TryCastAbility("lobAttack", GetComponent<Entity>(), targeting.position);
            }
            //Aim Lob
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
            }*/
            //Fire straight shot
/*            if (Input.GetKey(KeyCode.Q))
            {
                AbilitiesDirectory.TryCastAbility("shootAttack", GetComponent<Entity>());
            }
            //Fire spine attack
            if (Input.GetKey(KeyCode.E))
            {
                AbilitiesDirectory.TryCastAbility("spineAttack", GetComponent<Entity>());
            }*/
        }
    }

    [PunRPC]
    private void Fire(Vector3 pos)
    {

        // WE NEED INPUT MANAGER FOR MULTIPLE ABILITIES
        // Just one for now to test


        // NEW
        //abilities.basicAttack(pos);
        Debug.Log("Calling shoot ability!");



        abilities.TryCastAbility("shootAttack", pos);
    }


}
