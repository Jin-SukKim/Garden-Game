using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraight : BulletController
{
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
