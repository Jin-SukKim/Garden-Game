using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIEndpoint : MonoBehaviour {

    // cloud endpoints
    public static string CreateAccount = "https://techprobcit.azurewebsites.net/api/account/create";
    public static string Login = "https://techprobcit.azurewebsites.net/api/login/{0}/{1}";
    public static string UpdateStats = "https://techprobcit.azurewebsites.net/api/game";
    public static string GetLeaderboard = "https://techprobcit.azurewebsites.net/api/leaderboard";

    // local endpoints
    //public static string CreateAccount = "localhost:54882/api/account/create";
    //public static string Login = "localhost:54882/api/login/{0}/{1}";
    //public static string UpdateStats = "https://techprobcit.azurewebsites.net/api/game";
    //public static string GetLeaderboard = "localhost:54882/api/leaderboard";

}
