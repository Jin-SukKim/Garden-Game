using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The idea is to use this component as the main reference for any player/npc/plant
// it will have access to all the basic shared components like movement, health, abilities, etc.
public class Entity : MonoBehaviour
{
    public Teams.Faction team;
    public DamageSystem health;
    public Abilities abilities;
    public Transform spawnPoint;

    public GameObject respawnPoint;
    public float respawnPointWait;

    public MeshRenderer renderer;
    public CapsuleCollider collider;

    public bool respawning;
    public bool canMove;
    public bool canAct;
    public bool isPlayer;

    private void Start()
    {
        // Now the core components of the player can be accessed through the single entity component
        team = GetComponent<Teams>().TeamsFaction;
        health = GetComponent<DamageSystem>();
        abilities = GetComponent<Abilities>();
        spawnPoint = gameObject.transform.Find("spawnPoint");
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<CapsuleCollider>();
    }

    public void DeactivatePlayer()
    {
        collider.enabled = false;
        renderer.enabled = false;
        respawning = true;
    }

    public void Despawn()
    {
        DeactivatePlayer();
        //this.StartCoroutine(RespawnCoroutine(Time.time + 2f, this));
        StartCoroutine(WaitingRespawn(Time.time + respawnPointWait));
        //StartCoroutine(routine(Time.time));
    }

    /// <summary>
    /// Respawn function that moves the object to the respawn point and resets health
    /// </summary>
    public void Respawn()
    {
        transform.position = respawnPoint.transform.position;
        health.currentHealth = health.health;
        ReactivatePlayer();
    }

    public void ReactivatePlayer()
    {
        collider.enabled = true;
        renderer.enabled = true;
        respawning = false;
    }


    /// <summary>
    /// Waiting function for respawn that waits before respawning the object
    /// </summary>
    /// <param name="timeStamp">the given timestamp for the entity to respawn</param>
    /// <returns></returns>
    public IEnumerator WaitingRespawn(float timeStamp)
    {
        while (Time.time < timeStamp)
        {
            // wait for spawn timer
            yield return null;

        }

        
        Respawn();

        yield return null;
    }
}
