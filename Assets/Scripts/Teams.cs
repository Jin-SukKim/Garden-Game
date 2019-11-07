using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    public enum Faction { druid = 0, indust = 1}

    public Teams.Faction teamsFaction;
    public Teams.Faction TeamsFaction { get { return teamsFaction; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Teams(Teams.Faction team)
    {
        teamsFaction = team;
    }
}
