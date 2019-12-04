/*
 * Entity is a core component of any player/minion/plant during gametime, it contains essential information specific to each player/minion/plant
 * Authors: Thompson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The idea is to use this component as the main reference for any player/npc/plant
// it will have access to all the basic shared components like movement, health, abilities, etc.
public class Entity : MonoBehaviour
{
    public AudioSource audioSource;
    public Teams.Faction Team
    {
        get
        {
            return gameObject.GetComponent<Teams>().teamsFaction;
        }
    }
    public DamageSystem health;
    public int playerNum;
    public Abilities abilities;
    public Transform spawnPoint;
    public EntityResources resources;
    public GameObject respawnPoint;
    public
    
    const float respawnPointWait = 2f;

    //public MeshRenderer renderer;
    public CapsuleCollider collider;

    public bool respawning;
    public bool isPlayer;

    public bool IsDisabled;
    public bool IsPlanting;
    private bool canCast;
    public bool CanCast
    {
        get
        {
            return canCast && !IsDisabled && !IsPlanting;
        }
        set
        {
            canCast = value;
        }
    }
    private bool canMove;
    public bool CanMove
    {
        get
        {
            return canMove && !IsDisabled;
        }
        set
        {
            canMove = value;
        }
    }
    private void Start()
    {
        // Now the core components of the player can be accessed through the single entity component
        audioSource = GetComponent<AudioSource>();
        // team = GetComponent<Teams>().TeamsFaction;
        health = GetComponent<DamageSystem>();
        abilities = GetComponent<Abilities>();
        spawnPoint = gameObject.transform.Find("spawnPoint");
        IsDisabled = false;
        CanCast = true;
        CanMove = true;
        resources = GetComponent<EntityResources>();
        //renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<CapsuleCollider>();
    }

    public void DeactivatePlayer()
    {
        collider.enabled = false;
        //renderer.enabled = false;
        respawning = true;
    }

    public void Despawn()
    {
        DeactivatePlayer();
        //this.StartCoroutine(RespawnCoroutine(Time.time + 2f, this));
        transform.position = new Vector3(-999, -999, -999);
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
        //renderer.enabled = true;
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
