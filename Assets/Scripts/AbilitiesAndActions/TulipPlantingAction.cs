using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulipPlantingAction : IAction
{
    private string mObjectID = "mortartulip";
    public void doAction(Entity e, Ability a)
    {
        GameObject plant = PrefabManager.SpawnPlaceable(mObjectID, a.GetTargetPosition(), e);
        plant.GetComponent<PlaceableEntity>().moveThis();
        plant.GetComponent<PlaceableEntity>().Placed = true;
    }
}
