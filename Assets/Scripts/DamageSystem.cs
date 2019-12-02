/*
 * Game manager script, takes care of spawning
 * Authors: Marvin, Zora
 */

using Photon.Pun;
using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSystem : MonoBehaviourPun
{
    public Entity entity;
    public float health;
    public Image image;

    public float currentHealth;
    //public bool respawn = false;

    public GameObject enemeyDeathAnimation;
    //public GameObject respawnPoint;
    //public GameObject respawnPointWait;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    /// <summary>
    /// Determines if the object is still alive and destroys it if health is lower than 0
    /// </summary>
    void Update()
    {
        if (currentHealth <= 0 && !entity.isPlayer)
        {
            this.photonView.RPC("death", RpcTarget.AllBuffered);
        }
        else if (currentHealth <= 0 && !entity.respawning)
        {
            Debug.Log("CALLING RPC FOR DESPAWN FROM DAMAGESYS");
            //gameObject.transform.position = respawnPointWait.transform.position;
            //StartCoroutine(waitingRespawn());
            this.photonView.RPC("despawnOnDeath", RpcTarget.AllBuffered);
        }
    }

    //handle destroy player object on network
    [PunRPC]
    void death()
    {
        Debug.Log("DESTROYING OBJECT");
        GameObject obj = (GameObject)Instantiate(enemeyDeathAnimation, transform.position, Quaternion.identity);
        ////Destroy(gameObject.GetComponent("Rigidbody"));
        Destroy(obj, 5f);
        Destroy(gameObject);
    }

    [PunRPC]
    void despawnOnDeath()
    {
        Debug.Log("CALLING DESPAWN RPC");
        GameObject obj = (GameObject)Instantiate(enemeyDeathAnimation, transform.position, Quaternion.identity);
        Destroy(obj, 5f);
        entity.Despawn();
    }

    /// <summary>
    /// Decreases health amount by damage value parameter
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        Debug.Log("OW");
        currentHealth -= damage;
    }

    /// <summary>
    /// this function as been move to Entity
    /// </summary>
    //public void Respawn()
    //{
    //    if (respawn)
    //    {
    //        gameObject.transform.position = respawnPoint.transform.position;
    //        currentHealth = health;
    //    }
    //}

    /// <summary>
    /// this function as been move to Entity
    /// </summary>
    /// <returns></returns>
    //IEnumerator waitingRespawn()
    //{
    //    yield return new WaitForSeconds(3);
    //    Respawn();
    //}
}
