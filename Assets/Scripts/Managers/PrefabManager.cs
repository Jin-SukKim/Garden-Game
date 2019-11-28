using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// this static class is used to access prefabs like bullets from anywhere in the code
public static class PrefabManager
{
    static Dictionary<string, GameObject> bullets = new Dictionary<string, GameObject>();

    static Dictionary<string, GameObject> placeables = new Dictionary<string, GameObject>();

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

    // loads all the placeables from the placeables folder in the resources folder
    public static void LoadPlaceables()
    {
        GameObject[] allBullets = Resources.LoadAll("Prefabs/Placeables", typeof(GameObject)).Cast<GameObject>().ToArray();

        // Store all bullets in the dictionary with name as key
        for (int i = 0; i < allBullets.Length; i++)
        {
            bullets.Add(allBullets[i].name, allBullets[i]);
        }
    }

    /*    public static bool SpawnBullet(string bulletID, Vector3 pos, Quaternion rot, Entity e)
        {

            if (bullets.ContainsKey(bulletID))
            {
    *//*            Quaternion dir = Quaternion.LookRotation(targetLoc);*//*
                GameObject newObj = GameObject.Instantiate(bullets[bulletID], pos, dir);
                newObj.transform.rotation = Quaternion.Slerp(newObj.transform.rotation, dir, 0f);
                BulletController newBullet = newObj.GetComponent<BulletController>();
    *//*            newBullet.entity = e;*//*
                return true;
            } else
            {
                return false;
            }
        }*/

    public static bool SpawnBullet(string bulletID, Vector3 pos, Vector3 targetLoc, Entity e)
    {
        if (bullets.ContainsKey(bulletID))
        {
            Quaternion dir = Quaternion.LookRotation(targetLoc);
            GameObject newObj = GameObject.Instantiate(bullets[bulletID], pos, dir);
            BulletController newBullet = newObj.GetComponent<BulletController>();
            newBullet.InitBullet(pos, targetLoc);

            //not the best way to do this
            newBullet.entity = e;
            return true;
        }
        else
        {
            return false;
        }
    }


    public static bool SpawnPlaceable(string objectID, Vector3 pos, Vector3 targetLoc, Entity e)
    {

        if (placeables.ContainsKey(objectID))
        {
            Quaternion dir = Quaternion.LookRotation(targetLoc);
            GameObject newObj = GameObject.Instantiate(placeables[objectID], pos, dir);
            //BulletController newObject = newObj.GetComponent<BulletController>();
            //newBullet.InitBullet(pos, targetLoc);

            //not the best way to do this
            //newBullet.entity = e;
            return true;
        }
        else
        {
            return false;
        }
    }
}
