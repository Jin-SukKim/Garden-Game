using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosePlantingAction : IAction
{
    private string mObjectID = "shootrose";
    public void doAction(Entity e, Ability a)
    {
        PrefabManager.SpawnPlaceable(mObjectID, a.GetTargetPosition(), e);
    }
}
