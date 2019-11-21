using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lobs an object with a bullet id of "BulletLob" to a target location
/// </summary>
public class LobAction : IAction
{
    public string mBulletID = "BulletLob";
    public void doAction(Entity e, Ability a)
    {
        PrefabManager.SpawnBullet(mBulletID, e.transform.position, a.GetTargetPosition(), 0);
    }
}