using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PrefabManager
{
    static Dictionary<string, GameObject> bullets = new Dictionary<string, GameObject>();
    
    static PrefabManager()
    {
        LoadBullets();
    }

    public static void LoadBullets()
    {
        GameObject[] allBullets = Resources.LoadAll("Prefabs/Bullets",typeof(GameObject)).Cast<GameObject>().ToArray();

        // Store all bullets in the dictionary with name as key
        for (int i = 0; i < allBullets.Length; i++)
        {
            Debug.Log(allBullets[i].name);
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
