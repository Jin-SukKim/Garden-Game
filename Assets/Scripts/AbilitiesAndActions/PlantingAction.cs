using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingAction : IAction
{
    public void doAction(Entity e, Ability a)
    {
        e.IsPlanting = !e.IsPlanting;
        
        if (e.IsPlanting)
        {
            e.gameObject.AddComponent<PlantingSystem>();
        }
    }
}
