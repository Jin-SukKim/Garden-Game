using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobAction : IAction
{
    public string mBulletID = "BulletLob";
    public void doAction(Entity e, Ability a)
    {
        PrefabManager.SpawnBullet(mBulletID, e.transform.position, a.GetTargetPosition(), 0);
    }
}