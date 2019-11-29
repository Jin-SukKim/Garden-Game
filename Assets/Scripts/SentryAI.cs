using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public float turnSpeed = 10f;
    public SentryGun theGun;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        //theGun = GameObject.Find("OfflineGun").GetComponent<OfflineGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            theGun.isFiring = false;
            return;
        }

        //Vector3 lookVector = target.position - transform.position;
        //Quaternion rot = Quaternion.LookRotation(lookVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        Vector3 lookVector = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookVector);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        theGun.isFiring = true;

    }

    void UpdateTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestPlayer = null;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer != null && shortestDistance <= range)
        {
            target = nearestPlayer.transform;
        }
        else
        {
            target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
