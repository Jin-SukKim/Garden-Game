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
        Debug.Log("Shotgun dir: " + dir);
        for(int i = 0; i < bulletCount; i++)
        {
            //Debug.LogFormat("Shooting bullet {0}",i);

            Vector3 randDir = dir + new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f));
            Debug.Log(randDir);
            PrefabManager.SpawnBullet("Bullet",e.transform.position,randDir,0);
        }
    }
}
