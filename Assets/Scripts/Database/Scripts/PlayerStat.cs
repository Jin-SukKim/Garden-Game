using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour {
    private static PlayerStat instance;

    /// <summary>
    /// Property that creates a PlayerStat GameObject.
    /// </summary>
    public static PlayerStat Instance {
        get { return instance ?? (instance = new GameObject("PlayerStat").AddComponent<PlayerStat>()); }
    }

    /// <summary>
    /// Prints all of the values in the dictionary to the console. Use to
    /// test the functionality of this class.
    /// </summary>
    public void printAll() {
        foreach (KeyValuePair<string, string> entry in StatValues) {
            Debug.Log($"{entry.Key}: {entry.Value}");
        }
    }

    /// <summary>
    /// Stores all of the users stats for a given game.
    /// </summary>
    public Dictionary<string, string> StatValues = new Dictionary<string, string>();

    /// <summary>
    /// Stores information regarding the status of the most recent game played. Stores
    /// if the player has won or lost and which character they played as.
    /// </summary>
    public void addWinLoseData() {

        // TESTING
        win = true;
        druid = true;

        // Checks if the user won the game and which
        // character the user played as.
        if (win == true && druid == true) {
            StatValues.Add("winDruid", "1");
            StatValues.Add("winIndustrialist", "0");
            StatValues.Add("loseDruid", "0");
            StatValues.Add("loseIndustrialist", "0");
        } else if (win == true && industrialist == true) {
            StatValues.Add("winIndustrialist", "1");
            StatValues.Add("winDruid", "0");
            StatValues.Add("loseDruid", "0");
            StatValues.Add("loseIndustrialist", "0");
        } else if (win == false && druid == true) {
            StatValues.Add("loseDruid", "1");
            StatValues.Add("winDruid", "0");
            StatValues.Add("winIndustrialist", "0");
            StatValues.Add("loseIndustrialist", "0");
        } else if (win == false && industrialist == true) {
            StatValues.Add("loseIndustrialist", "1");
            StatValues.Add("winDruid", "0");
            StatValues.Add("winIndustrialist", "0");
            StatValues.Add("loseDruid", "0");
        } else {
            StatValues.Add("loseIndustrialist", "0");
            StatValues.Add("winDruid", "0");
            StatValues.Add("winIndustrialist", "0");
            StatValues.Add("loseDruid", "0");
        }
    }

    /// <summary>
    /// Stores the data from the most recent game played. 
    /// </summary>
    public void addPlayerData() {
        StatValues.Add("plantsPlanted", plantsPlanted.ToString());
        StatValues.Add("plantBulletsFired", plantBulletsFired.ToString());
        StatValues.Add("plantBulletHits", plantBulletHits.ToString());
        StatValues.Add("plantHitsTaken", plantHitsTaken.ToString());
        StatValues.Add("plantBarrierDeaths", plantBarrierDeaths.ToString());
        StatValues.Add("shotsFired", shotsFired.ToString());
        StatValues.Add("playerHits", playerHits.ToString());
        StatValues.Add("minionHits", minionHits.ToString());
        StatValues.Add("minionsSacrificed", minionsSacrificed.ToString());
        StatValues.Add("killsWorldTree", killsWorldTree.ToString());
        StatValues.Add("killsPlants", killsPlants.ToString());
        StatValues.Add("killsDruid", killsDruid.ToString());
        StatValues.Add("timePlayedDruid", timePlayedDruid.ToString());
        StatValues.Add("timePlayedIndustrialist", timePlayedIndustrialist.ToString());
    }

    /// <summary>
    /// Stores the data from the most recent game played. 
    /// </summary>
    public void addPlayerDataTESTING() {
        StatValues.Add("plantsPlanted", "5");
        StatValues.Add("plantBulletsFired", "5");
        StatValues.Add("plantBulletHits", "5");
        StatValues.Add("plantHitsTaken", "5");
        StatValues.Add("plantBarrierDeaths", "5");
        StatValues.Add("shotsFired", "5");
        StatValues.Add("playerHits", "5");
        StatValues.Add("minionHits", "5");
        StatValues.Add("minionsSacrificed", "5");
        StatValues.Add("killsWorldTree", "5");
        StatValues.Add("killsPlants", "5");
        StatValues.Add("killsDruid", "5");
        StatValues.Add("timePlayedDruid", "5");
        StatValues.Add("timePlayedIndustrialist", "5");
    }

    /// <summary>
    /// Property that is used to know if the user won or lost the game.
    /// </summary>
    public bool win {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to know if the user played as a druid.
    /// </summary>
    public bool druid {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to know if the user played as an industrialist.
    /// </summary>
    public bool industrialist {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the number of plants planted during a game.
    /// </summary>
    public int plantsPlanted {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the number of plant bullets that where fired during a game.
    /// </summary>
    public int plantBulletsFired {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the number of plant bullets that hit the opposing team during
    /// a game.
    /// </summary>
    public int plantBulletHits {
        get;
        set;
    }

    /// <summary>
    ///  Property that is used to store the damage taken by a plant during a game.
    /// </summary>
    public int plantHitsTaken {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the number of plant barrier deaths during a game.
    /// </summary>
    public int plantBarrierDeaths {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the amount of time a user has played as a druid.
    /// </summary>
    public int timePlayedDruid {
        get;
        set;
    }

    /// <summary>
    /// Property that is used to store the overall time a user has played.
    /// </summary>
    public int timePlayedOverall {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of shots fired by the user during a game.
    /// </summary>
    public int shotsFired {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of hits a user has during a game.
    /// </summary>
    public int playerHits {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of minion hits during a game.
    /// </summary>
    public int minionHits {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of minions sacrificed during a game.
    /// </summary>
    public int minionsSacrificed {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of world tree deaths during a game.
    /// </summary>
    public int killsWorldTree {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number plants killed by a user during a game.
    /// </summary>
    public int killsPlants {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the amount of time a user has spent playing as an industrialist
    /// </summary>
    public int timePlayedIndustrialist {
        get;
        set;
    }

    /// <summary>
    /// Property that tracks the number of times a user has killed a druid.
    /// </summary>
    public int killsDruid {
        get;
        set;
    }

}
