using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetLeaderboardScript : MonoBehaviour {

    // <summary>
    /// Starts a coroutine to query the database.
    /// </summary>
    public void GetLeaderBoard() {
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
        foreach (LeaderboardEntry entry in lb.LeaderboardEntries) {
            Debug.Log(entry.Username);
        }
    }

}
