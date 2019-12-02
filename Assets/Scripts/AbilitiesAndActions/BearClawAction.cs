using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearClawAction : IAction
{

    string mBulletID = "BearClaw";
    public void doAction(Entity e, Ability a)
    {
        Vector3 dir = a.GetTargetPosition() - e.transform.position;
        PrefabManager.SpawnBullet(mBulletID, e.spawnPoint.position, dir, e);
    }

}
