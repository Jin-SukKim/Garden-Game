using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAction : IAction
{
    string mBulletId = "BulletRound";
    int bulletCount = 6;

    public void doAction(Entity e, Ability a)
    {
        //Debug.Log("Shooting a shotgun");

        Vector3 targetPos = e.transform.position + (5 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
        
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 randDir = dir + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            PrefabManager.SpawnBullet(mBulletId,e.spawnPoint.position,randDir,e);
            
            GameObject.Instantiate(Resources.Load("Prefabs/ParticleEffects/shotgunFire"), e.spawnPoint.position, Quaternion.LookRotation(randDir));
        }
    }
}
