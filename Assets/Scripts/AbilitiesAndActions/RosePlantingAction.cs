using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RosePlantingAction : IAction
{
    private string mObjectID = "shootRose";
    public void doAction(Entity e, Ability a)
    {
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        PrefabManager.SpawnPlaceable(mObjectID, e.spawnPoint.position, a.GetTargetPosition(), e);
    }
}
