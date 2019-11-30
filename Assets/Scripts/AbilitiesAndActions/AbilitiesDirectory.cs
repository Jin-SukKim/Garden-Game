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
        MakeSpawnAbility();
        MakeShotgunAbility();
        MakeLobAbility();
        MakeShootAbility();
        MakeSpineAbility();
        MakeBulldozerAbility();
        MakePlantingAbility();
        MakeRosePlantingAbility();
        MakeBearTransformAbility();
        MakeBearClawAbility();
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
    public static Ability.AbilityFeedback TryCastAbility(string abilityID, Entity caster, Vector3 targetPosition, AbilityCastInfo info)
    {
        //try
        //{
        //    return abilityDictionary[abilityID].TryCastAbility(caster, targetPosition, info);
        //} catch (Exception e)
        //{
        //    Debug.Log("ABILITY SHOTGUN : " + abilityID);
        //    Debug.Log((abilityDictionary[abilityID] == null) ? "ability is NOT null" : "ability is NULL");
        //    throw e;
        //}
        return abilityDictionary[abilityID].TryCastAbility(caster, targetPosition, info);
    }


    // Tries to cast a specific ability without a range
    // returns a specific AbilityFeedback enum of the ability is out of range, on cooldown, or there isnt enough resource to use
    public static Ability.AbilityFeedback TryCastAbility(string abilityID, Entity caster, AbilityCastInfo info)
    {
        try
        {
            return abilityDictionary[abilityID].TryCastAbility(caster, info);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    // Tries to cast a specific ability
    // returns a specific AbilityFeedback enum of the ability is out of range, on cooldown, or there isnt enough resource to use
    /*    public static Ability.AbilityFeedback TryCastAbility(string abilityID, Entity caster, Vector3 targetPosition, AbilityCastInfo info)
        {
            try
            {
                return abilityDictionary[abilityID].TryCastAbility(caster, targetPosition, info);
            }
            catch (Exception e)
            {
                throw e;
            }*/
    //}

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
        Debug.Log((abilityDictionary["shotgunAttack"] == null) ? "ability is NOT null" : "ability is NULL");
    }

    public static void MakeLobAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new LobAction());

        Ability ability = new Ability(0.5f, 999, actions);
        abilityDictionary.Add("lobAttack", ability);
    }

    public static void MakeShootAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new ShootAction());

        Ability ability = new Ability(0.5f, 999, actions);
        abilityDictionary.Add("shootAttack", ability);
    }

    public static void MakeSpineAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new SpineAction());

        Ability ability = new Ability(1f, 999, actions);
        abilityDictionary.Add("spineAttack", ability);
    }

    public static void MakePlantingAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new PlantingAction());

        Ability ability = new Ability(0.5f, 999, actions);
        abilityDictionary.Add("placementActivate", ability);
    }

    public static void MakeRosePlantingAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new RosePlantingAction());

        Ability ability = new Ability(0.5f, 999, actions);
        abilityDictionary.Add("rosePlant", ability);
    }


    public static void MakeSpawnAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new SpawnAction());

        Ability ability = new Ability(0.5f, 999, actions);
        abilityDictionary.Add("spawnAttack", ability);
    }
  
    public static void MakeBulldozerAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new BulldozerSpawnAction());
        actions.Add(new DashAction());

        Ability ability = new Ability(4f, 999, actions);
        abilityDictionary.Add("bulldozerAttack", ability);
    }

    public static void MakeBearTransformAbility()
    {
        Debug.Log("MADE THE BEAR TRANSFORM ABILITY");
        List<IAction> actions = new List<IAction>();
        actions.Add(new BearAction());

        Ability ability = new Ability(25f, 0, actions);
        abilityDictionary.Add("bearTransform", ability);
    }

    public static void MakeBearClawAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new BearClawAction());

        Ability ability = new Ability(0.8f, 5, actions);
        abilityDictionary.Add("bearClawAttack", ability);
    }
}

