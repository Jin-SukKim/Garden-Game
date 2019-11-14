using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * A script to make game objects have a billboarding effect (always face the camera)
 * To use: assign this script to the gameobject that will face the camera 
 */

public class BillboardEffect : MonoBehaviour
{
    public Camera mainCamera;

    void LateUpdate()
    {
        transform.LookAt(
            transform.position + mainCamera.transform.rotation *
            Vector3.forward, mainCamera.transform.rotation * Vector3.up
            );
    }
}