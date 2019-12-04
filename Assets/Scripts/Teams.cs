using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    // Enumerator for if either a druid or an industrialist
    public enum Faction { druid = 0, indust = 1}

    public Teams.Faction teamsFaction;
    public Teams.Faction TeamsFaction { get { return teamsFaction; } }

    /// <summary>
    /// Team constructor for an object
    /// </summary>
    /// <param name="team"></param>
    public Teams(Teams.Faction team)
    {
        teamsFaction = team;
    }
}
