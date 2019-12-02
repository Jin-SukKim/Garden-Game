using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerSpawnAction : IAction
{
    string mBulletID = "Bulldozer";
    public void doAction(Entity e, Ability a)
    {
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.spawnPoint.position, dir, e);
    }
}
