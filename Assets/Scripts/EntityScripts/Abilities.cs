using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    //List<string> abilityIDs = new List<string>();
    Entity owner;

    string basicAttack = "shotgun";

    List<string> abilityIDs = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AddAbility(string abilityID)
    {
        if (abilityIDs.Contains(abilityID))
            return false;

        abilityIDs.Add(abilityID);
        return true;
    }

    public Ability.AbilityFeedback TryCastAbility(string abilityID, Vector3 targetPosition)
    {
        return AbilitiesDirectory.TryCastAbility(abilityID, owner, targetPosition);
    }


    //Ability.AbilityFeedback TryAttack(Vector3 targetPosition)
    //{
    //    TryCastAbility('attack')
    //}
}
