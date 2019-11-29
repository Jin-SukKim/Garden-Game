using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a component that stores all the abilities of a unit/player/plant
// it consists of a bunch of ability IDs and the actual ability is store in a static abilities directory class
public class Abilities : MonoBehaviour
{
    Entity owner;

    [SerializeField]
    private List<string> abilityIDs = new List<string>();

    [SerializeField]
    private List<string> plantIDs = new List<string>();

    private Dictionary<string, AbilityCastInfo> castInfos = new Dictionary<string, AbilityCastInfo>();

    private Dictionary<string, AbilityCastInfo> plantInfos = new Dictionary<string, AbilityCastInfo>();

    private void Start()
    {
        owner = GetComponent<Entity>();
        foreach(string s in abilityIDs)
        {
            AbilityCastInfo info = new AbilityCastInfo();
            castInfos.Add(s, info);
        }

        foreach (string s in plantIDs)
        {
            AbilityCastInfo info = new AbilityCastInfo();
            plantInfos.Add(s, info);
        }
    }

    public bool AddAbility(string abilityID)
    {
        if (abilityIDs.Contains(abilityID))
            return false;

        abilityIDs.Add(abilityID);
        return true;
    }

    // Tries to cast a specific ability
    // returns a specific AbilityFeedback enum of the ability is out of range, on cooldown, or there isnt enough resource to use
    public Ability.AbilityFeedback TryCastAbility(string abilityID, Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(abilityID, owner, targetPosition, castInfos[abilityID]);
    }

    public Ability.AbilityFeedback basicAttack(Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(abilityIDs[0], owner, targetPosition, castInfos[abilityIDs[0]]);
    }

    public Ability.AbilityFeedback castAbility(int index, Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(abilityIDs[index], owner, targetPosition, castInfos[abilityIDs[0]]);
    }

    public Ability.AbilityFeedback plantAbility(int index, Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(plantIDs[index], owner, targetPosition, plantInfos[plantIDs[0]]);
    }

    //public float GetAbilityTimestamp()
    //{

    //}

}
