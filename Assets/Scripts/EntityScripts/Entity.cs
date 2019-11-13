using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The idea is to use this component as the main reference for any player/npc/plant
// it will have access to all the basic shared components like movement, health, abilities, etc.
public class Entity : MonoBehaviour
{
    Movement movement;
    //Team team
    DamageSystem health;
    Abilities abilities;
}
