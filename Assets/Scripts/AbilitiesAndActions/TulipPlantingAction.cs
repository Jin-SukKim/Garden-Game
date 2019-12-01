using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TulipPlantingAction : IAction
{
    private string mObjectID = "mortartulip";
    public void doAction(Entity e, Ability a)
    {
        PrefabManager.SpawnPlaceable(mObjectID, a.GetTargetPosition(), e).GetComponent<PlaceableEntity>().moveThis();
    }
}
