using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This interface is for any sort of action that is done for an ability
// like shooting, casting a spell, or dashing
public interface IAction {

    // the position of the caster can be obtained through Entity e
    // the target position of the ability (if it has one) can be obtained through Ability a
    void doAction(Entity e, Ability a);
}
