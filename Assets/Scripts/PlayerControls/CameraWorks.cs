/*
 * Follows the player during game, and corrects the walkable axis since our camera is angled 45 degrees to Unity axes
 * Authors: Zora
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWorks : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void Update()
    {
        if(target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            transform.LookAt(target);
        }
        
    }
}
