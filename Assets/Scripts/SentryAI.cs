using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryAI : MonoBehaviour
{
    public Transform target;
    public float range = 15f;
    public float turnSpeed = 10f;

    public Entity entity;
    public Abilities abilities;

    // Start is called before the first frame update
    void Start()
    {
        abilities = gameObject.GetComponent<Abilities>();
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        //theGun = GameObject.Find("OfflineGun").GetComponent<OfflineGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        //Vector3 lookVector = target.position - transform.position;
        //Quaternion rot = Quaternion.LookRotation(lookVector);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);

        Vector3 lookVector = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookVector);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        abilities.TryCastAbility("shootAttack", target.position);

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
            if (obj.GetComponent<Entity>().team == Teams.Faction.indust && obj.tag == "Player")
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
