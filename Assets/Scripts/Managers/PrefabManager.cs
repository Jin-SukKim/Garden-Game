﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// this static class is used to access prefabs like bullets from anywhere in the code
public static class PrefabManager
{
    static Dictionary<string, GameObject> bullets = new Dictionary<string, GameObject>();
    
    // static constructor for any initialization
    static PrefabManager()
    {
        LoadBullets();
    }

    // loads all the bullets from the bullets folder in the resources folder
    public static void LoadBullets()
    {
        GameObject[] allBullets = Resources.LoadAll("Prefabs/Bullets",typeof(GameObject)).Cast<GameObject>().ToArray();

        // Store all bullets in the dictionary with name as key
        for (int i = 0; i < allBullets.Length; i++)
        {
            bullets.Add(allBullets[i].name, allBullets[i]);
        }
    }

    public static bool SpawnBullet(string bulletID, Vector3 pos, Quaternion rot, int team)
    {

        if (bullets.ContainsKey(bulletID))
        {
            GameObject newObj = GameObject.Instantiate(bullets[bulletID], pos, rot);
            BulletController newBullet = newObj.GetComponent<BulletController>();
            return true;
        } else
        {
            return false;
        }
    }

}