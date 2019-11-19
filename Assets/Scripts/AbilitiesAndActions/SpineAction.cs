using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineAction : IAction
{
    public string mBulletID = "BulletSpine";
    public void doAction(Entity e, Ability a)
    {
        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.transform.position, dir, 0);
    }
}