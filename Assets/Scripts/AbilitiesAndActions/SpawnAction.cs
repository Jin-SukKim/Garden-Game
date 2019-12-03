using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAction : IAction
{

    public SpawnAction()
    {

    }

    public void doAction(Entity e, Ability a)
    {
        

        Vector3 myVec = new Vector3(a.GetTargetPosition().x, a.GetTargetPosition().y + 2, a.GetTargetPosition().z);
        Vector3 targetPos = e.transform.position + (10 * e.transform.forward);
        Vector3 dir = targetPos - e.transform.position;
       
        GameObject.Instantiate(Resources.Load("Prefabs/ParticleEffects/MoneyParticles"), e.transform.position , Quaternion.LookRotation(dir));
        GameObject.Instantiate(Resources.Load("Prefabs/ParticleEffects/MinionSpawn"), myVec, Quaternion.identity);

        SoundManager.PlaySoundatLocation("Minion_Ping", e.transform.position);

        e.StartCoroutine(SpawnMinion(myVec, e));

    }
    public IEnumerator SpawnMinion(Vector3 myVec, Entity e)
    {
        yield return new WaitForSeconds(0.25f);
        PrefabManager.SpawnMinion(myVec, e);
        
    }
}
