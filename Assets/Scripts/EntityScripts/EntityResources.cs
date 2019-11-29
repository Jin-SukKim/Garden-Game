using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityResources : MonoBehaviour
{
    private Dictionary<string, IResource> resources;

    private void Start()
    {
        resources.Add("mana", new BasicResource(10f));
    }

    public IResource GetResource(string id)
    {
        return resources[id];
    }
}
