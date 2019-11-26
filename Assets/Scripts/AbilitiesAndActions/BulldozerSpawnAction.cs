using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerSpawnAction : IAction
{
    string mBulletID = "Bulldozer";
    public void doAction(Entity e, Ability a)
    {
        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.spawnPoint.position, dir, e);
    }
}
