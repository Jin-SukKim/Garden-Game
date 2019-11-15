using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This static class stores all the abilities in the game so we can access them from anywhere
// each abiltity has a string which acts as its ID in a string -> ability dictionary 
public static class AbilitiesDirectory
{
    static Dictionary<string, Ability> abilityDictionary = new Dictionary<string, Ability>();

    // static constructor for any initialization
    static AbilitiesDirectory()
    {
        // I'm not sure what's the best way to create abilities right now
        MakeShotgunAbility();
    }

    public static bool addAbility(string abilityID, Ability ability)
    {
        if (abilityDictionary.ContainsKey(abilityID))
            return false;

        abilityDictionary.Add(abilityID, ability);
        return true;
    }

    // Tries to cast a specific ability
    // returns a specific AbilityFeedback enum of the ability is out of range, on cooldown, or there isnt enough resource to use
    public static Ability.AbilityFeedback TryCastAbility(string abilityID, Entity caster, Vector3 targetPosition)
    {
        try
        {
            return abilityDictionary[abilityID].TryCastAbility(caster, targetPosition);
        } catch(Exception e)
        {
            Debug.Log("ABILITY SHOTGUN : " + abilityID);
            throw e;
        }
    }

    // I'm not sure what's the best way to create abilities right now
    // so this function creates the shotgun ability
    // should only be used once!
    public static void MakeShotgunAbility()
    {
        Debug.Log("MADE THE SHOTGUN ABILITY");
        List<IAction> actions = new List<IAction>();
        actions.Add(new ShotgunAction());
        
        Ability ability = new Ability(0.5f,999,actions);
        abilityDictionary.Add("shotgunAttack", ability);
    }
}
