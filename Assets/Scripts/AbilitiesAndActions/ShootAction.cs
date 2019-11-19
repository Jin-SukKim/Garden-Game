using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : IAction
{
    public string mBulletID = "BulletLob";
    public void doAction(Entity e, Ability a)
    {
        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.transform.position, dir, 0);
    }
}