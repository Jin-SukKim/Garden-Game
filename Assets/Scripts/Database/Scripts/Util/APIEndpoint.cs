using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIEndpoint : MonoBehaviour {

    public static string CreateAccountLocal = "localhost:54882/api/account/create";
    public static string CreateAccountCloud = "https://techpro2019.azurewebsites.net/api/account/create";
    public static string LoginLocal = "localhost:54882/api/login/{0}/{1}";
    public static string LoginCloud = "https://techpro2019.azurewebsites.net/api/login/{0/{1}}";
    public static string UpdateStatsLocal = "https://techpro2019.azurewebsites.net/api/login/{0/{1}}";

}
