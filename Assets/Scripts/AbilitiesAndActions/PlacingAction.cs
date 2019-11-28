using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingAction : MonoBehaviour
{
    public void doAction(Entity e, Ability a)
    {
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        /*
     * make entity IsPlanting
     * 
     */
        //PrefabManager.SpawnPlaceable(mObjectID, e.spawnPoint.position, a.GetTargetPosition(), e);
    }

}
