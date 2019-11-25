using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivatePlayer(Entity e)
    {
        e.collider.enabled = false;
        e.renderer.enabled = false;
        e.inputAllowed = false;
    }

    public void ReactivatePlayer(Entity e)
    {
        e.collider.enabled = true;
        e.renderer.enabled = true;
        e.inputAllowed = true;
    }
}
