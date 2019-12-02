/*
 * Base class for NPC behaviors
 * Authors: Zora
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for any behaviors that are implemented for NPCs
public class NPCBehavior : StateMachineBehaviour {

    public GameObject NPC;
    public GameObject mainTarget;

    // Not referencing correctly in children classes using base.speed...
    public float speed = 2.5f;

    //Targeting<UnityEngine.GameObject> targeting;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Assign the object that is passed in as the NPC object
        NPC = animator.gameObject;
    }
}
