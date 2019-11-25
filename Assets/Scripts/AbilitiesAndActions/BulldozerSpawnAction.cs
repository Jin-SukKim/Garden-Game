using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulldozerSpawnAction : IAction
{
    public void doAction(Entity e, Ability a)
    {
        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        //PrefabManager.SpawnBullet(mBulletId, e.spawnPoint.position, randDir, e);
    }
}
