using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIEndpoint : MonoBehaviour {

    public static string CreateAccountLocal = "localhost:54882/api/account/create";
    public static string CreateAccountCloud = "https://techprobcit.azurewebsites.net/api/account/create";
    public static string LoginLocal = "localhost:54882/api/login/{0}/{1}";
    public static string LoginCloud = "https://techprobcit.azurewebsites.net/api/login/{0/{1}}";
    public static string UpdateStatsLocal = "https://techprobcit.azurewebsites.net/api/login/{0/{1}}";

    public static string GetLeaderboardLocal = "localhost:54882/api/leaderboard";
    public static string GetLeaderboardCloud = "https://techprobcit.azurewebsites.net/api/leaderboard";

}
