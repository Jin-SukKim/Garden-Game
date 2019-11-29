using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

// This is a component that stores all the abilities of a unit/player/plant
// it consists of a bunch of ability IDs and the actual ability is store in a static abilities directory class
public class Abilities : MonoBehaviourPun
{
    Entity owner;

    [SerializeField]
    private List<string> abilityIDs = new List<string>();
    private List<string> animationIDs;

    private Dictionary<string, AbilityCastInfo> castInfos = new Dictionary<string, AbilityCastInfo>();


    [PunRPC]
    private void triggerAnim(string animString)
    {
        GetComponentInChildren<Animator>().SetTrigger(animString);
    }

    private void Start()
    {
        owner = GetComponent<Entity>();
        foreach(string s in abilityIDs)
        {
            AbilityCastInfo info = new AbilityCastInfo();
            castInfos.Add(s, info);
        }
        animationIDs = new List<string>();
        animationIDs.Add("BasicAttack");
        animationIDs.Add("Ability");
        animationIDs.Add("RallyOrPlant");
        animationIDs.Add("Ultimate");
    }

    public bool AddAbility(string abilityID)
    {
        if (abilityIDs.Contains(abilityID))
            return false;

        abilityIDs.Add(abilityID);
        return true;
    }
    
    //Overwrites an existing ability in specified slot.
    public bool SetAbility(string abilityID, int slot)
    {
        if(!castInfos.ContainsKey(abilityID))
        {
            AbilityCastInfo info = new AbilityCastInfo();
            castInfos.Add(abilityID, info);
        }

        if(abilityIDs.Count < 1)
        {
            Debug.Log("Error setting ability: No Slot Available");
            return false;
        }
        else
        {
            abilityIDs[slot] = abilityID;
            return true;
        }
    }

    //Get current ability from specified slot.
    public string GetAbility(int slot)
    {
        if (abilityIDs.Count < 1)
        {
            Debug.Log("Error getting ability: Slot Does Not Exist");
            return "null";
        }
        else
        {
            return abilityIDs[slot];
        }
    }

    // Tries to cast a specific ability
    // returns a specific AbilityFeedback enum of the ability is out of range, on cooldown, or there isnt enough resource to use
    public Ability.AbilityFeedback TryCastAbility(string abilityID, Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(abilityID, owner, targetPosition, castInfos[abilityID]);
    }

    public Ability.AbilityFeedback basicAttack(Vector3 targetPosition)
    {
        if (!PhotonNetwork.IsConnected)
        {
            triggerAnim(animationIDs[0]);
        }
        else
        {
            this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[0]);
        }
        return AbilitiesDirectory.TryCastAbility(abilityIDs[0], owner, targetPosition, castInfos[abilityIDs[0]]);
    }

    public Ability.AbilityFeedback castAbility(int index, Vector3 targetPosition)
    {
        // Left out for now during integration
        //GetComponentInChildren<Animator>().SetTrigger(animationIDs[index]);
        //return AbilitiesDirectory.TryCastAbility(abilityIDs[index], owner, targetPosition, castInfos[abilityIDs[index]]);

        if (!PhotonNetwork.IsConnected)
        {
            triggerAnim(animationIDs[index]);
        }
        else
        {
            this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[index]);
        }
        return AbilitiesDirectory.TryCastAbility(abilityIDs[index], owner, targetPosition, castInfos[abilityIDs[0]]);
    }

    //public float GetAbilityTimestamp()
    //{

    //}

}
