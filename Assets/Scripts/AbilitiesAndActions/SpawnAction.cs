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
        PrefabManager.SpawnMinion(myVec, e);

    }
}
