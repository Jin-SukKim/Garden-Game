using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability {   

    public enum AbilityFeedback { outOfRange, onCooldown, noResource, success};

    private float cost;
    private float cooldown;
    private float cooldownTimestamp;
    private float range;

    private IResource resource;

    private List<IAction> actions;
    private Vector3 targetPosition;

    public Ability(float cooldown, float range, List<IAction> actions)
    {
        this.cooldown = cooldown;
        this.range = range;
        this.actions = actions;
    }

    public virtual AbilityFeedback TryCastAbility(Entity e)
    {
        if (!checkCooldown())
            return AbilityFeedback.onCooldown;

        if (!checkCost())
            return AbilityFeedback.noResource;

        targetPosition = e.transform.position;

        fireAbility(e);
        return AbilityFeedback.success;
    }

    public virtual AbilityFeedback TryCastAbility(Entity e, Vector3 position) {

        if (!checkRange(e,position))
            return AbilityFeedback.outOfRange;

        targetPosition = position;

        if (!checkCooldown())
            return AbilityFeedback.onCooldown;

        if (!checkCost())
            return AbilityFeedback.noResource;
        
        fireAbility(e);
        return AbilityFeedback.success;
    }

    // CHECKS RESOURCE??????????
    protected bool checkCooldown()
    {
        return (resource == null || resource.ResourceCheck(cost));
    }

    protected bool checkCost() {
        return (cooldown == 0 || Time.time >= cooldownTimestamp);
    }

    protected bool checkRange(Entity e,Vector3 pos) {
        return (range == 0 || (Vector3.SqrMagnitude(e.transform.position - pos) <= range * range));
    }

    // need to check if no actions in list?????
    protected void fireAbility(Entity e) {
        payCostAndCoolDown();
        foreach(IAction a in actions) {
            a.doAction(e,this);
        }
    }

    protected void payCostAndCoolDown() {
        if (cooldown != 0) {
            cooldownTimestamp = Time.time + cooldown;
        }

        if (resource != null)
            resource.PayCost(cost);
    }

    public void SetResource(IResource r) {
        resource = r;
    }

    public void SetCooldown(float f) {
        cooldown = f;
    }

    public void SetCost(float f) {
        cost = f;
    }

    public float GetTimestamp() {
        return cooldownTimestamp;
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
    }
}
