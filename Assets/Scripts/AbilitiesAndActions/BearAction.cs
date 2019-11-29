using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAction : IAction
{
    public float duration = 8f;
    private int oldSkin;
    private string oldAbility;

    public void doAction(Entity e, Ability a)
    {
        //Get and store old skin XXXXXXXXXXXXXXXXX This needs to be changed as soon as Entities can distinguish which character they are.
        oldSkin = 4;

        //Apply skin
        SkinManager.Instance.applySkin(e.gameObject, 10);
        
        //Get and store original Primary Attack
        oldAbility = e.gameObject.GetComponent<Abilities>().GetAbility(0);
        if (oldAbility.Equals("null"))
        {
            Debug.Log("Error in BearAction: Cannot get oldAbility");
            return;
        }
        
        //Set player's primary attack to bearclaw
        bool gotBearClaw = e.gameObject.GetComponent<Abilities>().SetAbility("bearClawAttack", 0);
        if (!gotBearClaw)
        {
            Debug.Log("Error in BearAction: Cannot set new ability");
        }

        e.gameObject.GetComponent<DamageSystem>().health = 20f;
        e.gameObject.GetComponent<DamageSystem>().currentHealth += 10f;

        e.gameObject.GetComponent<Movement>().SetSpeed(8f);

        //Begin Duration
        e.StartCoroutine(bearTransform(e, a));
    }

    IEnumerator bearTransform(Entity e, Ability a)
    {
        //Wait until duration has ended
        yield return new WaitForSeconds(duration);

        e.gameObject.GetComponent<DamageSystem>().health = 10f;
        if (e.gameObject.GetComponent<DamageSystem>().currentHealth > 10f)
        {
            e.gameObject.GetComponent<DamageSystem>().currentHealth = 10f;
        }

        e.gameObject.GetComponent<Movement>().SetSpeed(4f);

        //Swap back to original skin
        SkinManager.Instance.applySkin(e.gameObject, oldSkin);

        //Set primary attack back to original.
        e.gameObject.GetComponent<Abilities>().SetAbility(oldAbility, 0);
    }
}
