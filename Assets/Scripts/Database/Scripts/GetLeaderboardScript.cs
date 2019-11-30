using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// This script is used to get the leaderboard and display it.
/// </summary>
// public class GetLeaderboardScript : MonoBehaviour {
//    [SerializeField]
//     public GameObject myPrefab;

//     void Start()
//     {
//         GetLeaderBoard();
//     }

//     /// <summary>
//     /// Starts a coroutine to query the database.
//     /// </summary>
//     public void GetLeaderBoard() {
//         string localUrl = APIEndpoint.GetLeaderboardLocal;
//         string cloudUrl = APIEndpoint.GetLeaderboardCloud;

//         //StartCoroutine(SendHttpRequest(localUrl));
//         StartCoroutine(SendHttpRequest(cloudUrl));
//     }

/// <summary>
/// This coroutine sends a get request to query the database which will return
/// the leaderboard as an object
public class GetLeaderboardScript : MonoBehaviour {

    // <summary>
    /// Starts a coroutine to query the database.
    /// </summary>
    public void GetLeaderBoard()
    {
        StartCoroutine(SendHttpRequest(APIEndpoint.GetLeaderboard));
    }

    /// <summary>
    /// This coroutine sends a post request to update the database with the stats saved at
    /// the end of each game.
    /// </summary>
    IEnumerator SendHttpRequest(string url) {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("error:");
            Debug.Log(request.error);
        } else {
            Debug.Log("success getting leaderboard");
            string response = request.downloadHandler.text;
            Debug.Log(response);
            //StatusText.text = request.downloadHandler.text;

            // wrap the response in a json object because unity's json library
            // does not handle top-level arrays...
            string wrappedJson = string.Format("{{\"LeaderboardEntries\":{0}}}", response);

            LeaderboardJsonWrapper lb = JsonUtility.FromJson<LeaderboardJsonWrapper>(wrappedJson);

            DisplayLeaderboard(lb);
        }
    }

    // <summary>
    /// Displays the leaderboard.
    /// </summary>
    public void DisplayLeaderboard(LeaderboardJsonWrapper lb) {
        //only get top 8 results
/*        int i = 0;
        foreach (LeaderboardEntry entry in lb.LeaderboardEntries) {
            if(i < 10)
            {
                Debug.Log(entry.Username);
                GameObject instance = Instantiate(myPrefab);

                //GameObject tmp = instance.Find("Text").GetComponent<Text>();
                //Debug.Log(tmp.);
                instance.GetComponentInChildren<Text>().text = entry.Username;
                instance.transform.SetParent(gameObject.transform, false);
                i++;
            }*/
        foreach (LeaderboardEntry entry in lb.LeaderboardEntries) {
            Debug.Log(entry.Username);
        }
    }

}
