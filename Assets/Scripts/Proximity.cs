using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity : MonoBehaviour
{
    //Trigger enter
    void OnTriggerEnter()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        Debug.Log("Entered");
    }

    //Trigger exit
    void OnTriggerExit()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
        Debug.Log("Exit");
    }
}
