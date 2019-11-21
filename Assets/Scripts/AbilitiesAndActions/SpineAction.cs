﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns a certain number of objects in succession with an id of "BulletSpine"
/// which are spawned further away the more objects are spawned
/// </summary>
public class SpineAction : IAction
{
    public string mBulletID = "BulletSpine";
    public int spineCount = 0;
    public int maxSpines = 6;
    public float timeInterval = 0.2f;
    public void doAction(Entity e, Ability a)
    {
        e.StartCoroutine(spineShoot(e, a));
    }
    //Enumerator used to shoot with a delay in between
    IEnumerator spineShoot(Entity e, Ability a)
    {
        Vector3 dir = e.transform.forward;
        Vector3 initalPos = e.transform.position;
        while (spineCount < maxSpines)
        {
            Vector3 targetPos = initalPos + (spineCount * dir) - e.transform.up;
            PrefabManager.SpawnBullet(mBulletID, targetPos, e.transform.up, 0);
            spineCount++;
            yield return new WaitForSeconds(timeInterval);
        }
        spineCount = 0;
    }
}