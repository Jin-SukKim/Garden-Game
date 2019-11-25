using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoots an object with a bullet id of "Bullet" in a certain direction
/// </summary>
public class ShootAction : IAction
{
    public string mBulletID = "Bullet";
    public void doAction(Entity e, Ability a)
    {
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.spawnPoint.position, dir, e);
    }
}