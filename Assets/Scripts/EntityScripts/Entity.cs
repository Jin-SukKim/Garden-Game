using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The idea is to use this component as the main reference for any player/npc/plant
// it will have access to all the basic shared components like movement, health, abilities, etc.
public class Entity : MonoBehaviour
{
    public Teams.Faction team;
    public DamageSystem health;
    public Abilities abilities;


    private void Start()
    {
        // Now the core components of the player can be accessed through the single entity component
        team = GetComponent<Teams>().TeamsFaction;
        health = GetComponent<DamageSystem>();
        abilities = GetComponent<Abilities>();
    }
}
