using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Shoots a Rrndom spread of bullets in a certain direction
/// </summary>
public class ShotgunAction : IAction
{
    string mBulletId = "BulletRound";
    int bulletCount = 6;

    public void doAction(Entity e, Ability a)
    {
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        for(int i = 0; i < bulletCount; i++)
        {
            Vector3 randDir = dir + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            PrefabManager.SpawnBullet(mBulletId,e.transform.position,randDir,0);
        }
    }
}
