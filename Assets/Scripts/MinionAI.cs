using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MinionAI : MonoBehaviour
{
    public Transform target;
    public float range = 5f;
    public float turnSpeed = 10f;
    private NavMeshAgent nma;

    public Abilities abilities;

    // Start is called before the first frame update
    void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        abilities = gameObject.GetComponent<Abilities>();
        /*entity.team = gameObject.getComponenct<Entity>().team;*/ //Set m
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        //theGun = GameObject.Find("OfflineGun").GetComponent<OfflineGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            //theGun.isFiring = false;
            return;
        }


        //script for looking
        Vector3 lookVector = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookVector);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //script for following
        

        //script for firing
        if (Vector3.Distance(transform.position, target.position) < 3)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            //transform.position = transform.position;
            //theGun.isFiring = true;
            abilities.TryCastAbility("shootAttack", target.position);


        } else
        {
            //transform.position += transform.forward * 2 * Time.deltaTime;
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            nma.SetDestination(target.transform.position);

        }

        //theGun.isFiring = true;

    }

    void UpdateTarget()
    {
        Entity[] scripts = GameObject.FindObjectsOfType<Entity>();
        GameObject[] objects = new GameObject[scripts.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = scripts[i].gameObject;
        }

        List<GameObject> players = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (obj.GetComponent<Entity>().Team == Teams.Faction.druid && obj.tag == "Player")
            {
                players.Add(obj);
            }
        }

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
