using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpine : BulletController
{
    public Vector3 startDir;
    public OfflineGunController owner;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = shootingLoc;
        transform.position += Vector3.down;
        lifeTime = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
