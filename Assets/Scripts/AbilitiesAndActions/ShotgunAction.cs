using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAction : IAction
{
    int bulletCount = 6;

    public void doAction(Entity e, Ability a)
    {
        //Debug.Log("Shooting a shotgun");

        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        for(int i = 0; i < bulletCount; i++)
        {
            //Debug.LogFormat("Shooting bullet {0}",i);

            Vector3 randDir = dir + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            PrefabManager.SpawnBullet("Bullet",e.transform.position,Quaternion.LookRotation(randDir, Vector3.up), e);
        }
    }
}
