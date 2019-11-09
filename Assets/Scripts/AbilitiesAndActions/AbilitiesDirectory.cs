using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilitiesDirectory
{
    static Dictionary<string, Ability> abilityDictionary = new Dictionary<string, Ability>();

    static AbilitiesDirectory()
    {
        MakeShotgunAbility();
    }

    public static bool addAbility(string abilityID, Ability ability)
    {
        if (abilityDictionary.ContainsKey(abilityID))
            return false;

        abilityDictionary.Add(abilityID, ability);
        return true;
    }

    public static Ability.AbilityFeedback TryCastAbility(string abilityID, Entity caster, Vector3 targetPosition)
    {
        try
        {
            return abilityDictionary[abilityID].TryCastAbility(caster, targetPosition);
        } catch(Exception e)
        {
            throw e;
        }
    }

    public static void MakeShotgunAbility()
    {
        List<IAction> actions = new List<IAction>();
        actions.Add(new ShotgunAction());


        Ability ability = new Ability(0.5f,999,actions);
        abilityDictionary.Add("shotgunAttack", ability);
    }
}
