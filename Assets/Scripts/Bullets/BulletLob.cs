using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of lobbing an object to a certain location from a certain location
/// </summary>
public class BulletLob : BulletController
{
    private float arcLifetime;

    private float animationStart;
    private Vector3 midPoint;


    // Start is called before the first frame update
    void Start()
    {
        arcLifetime = 2f;
        lifeTime = arcLifetime;
        animationStart = Time.time;
        targetLoc.y = 0;
        //The apex of the arc
        midPoint = ((shootingLoc + targetLoc) / 2) + new Vector3(0, 10f);
        Debug.Log(targetLoc);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float lerpVal = (Time.time - animationStart) / arcLifetime;

        Vector3 curve1 = Vector3.Lerp(shootingLoc, midPoint, lerpVal);
        Vector3 curve2 = Vector3.Lerp(midPoint, targetLoc, lerpVal);
        transform.position = Vector3.Lerp(curve1, curve2, lerpVal);
    }
}
