using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Sends stats to the database to be updated.
/// </summary>
public class PostGameScript : MonoBehaviour {

    /// <summary>
    /// Starts a coroutine to query the database.
    /// </summary>
    public void UpdateStats() {

        // check if player is logged in
        string username = PlayerPrefs.GetString("username", string.Empty);
        if (username == string.Empty) {
            //StatusText.text = "not logged in so not saving stats";
            return;
        }

        //StatusText.text = "updating stats...";

        string localUrl = APIEndpoint.UpdateStatsLocal;
        //string cloudUrl = string.Format(APIEndpoint.LoginCloud, UsernameInputField.text);

        StartCoroutine(SendHttpRequest(localUrl, username));
        //StartCoroutine(SendHttpRequest(cloudUrl));
    }

    /// <summary>
    /// This coroutine sends a post request to update the database with the stats saved at
    /// the end of each game.
    /// </summary>
    IEnumerator SendHttpRequest(string url, string username) {
        //PlayerStat.Instance.addPlayerData();     
        PlayerStat.Instance.addWinLoseData();
        PlayerStat.Instance.addPlayerDataTESTING();

        WWWForm postData = new WWWForm();
        postData.AddField("username", username);

        // iterate through saved stats and add them to the post body
        foreach (KeyValuePair<string, string> entry in PlayerStat.Instance.StatValues) {
            postData.AddField(entry.Key, entry.Value);
        }

        UnityWebRequest request = UnityWebRequest.Post(url, postData);
        request.SetRequestHeader("Content-Type", "text/plain");

        // get the user's jwt
        string token = PlayerPrefs.GetString("token", string.Empty);
        if (token == string.Empty) {
            //StatusText.text = "no jwt found";
            yield return 1;
        }

        request.SetRequestHeader("Authorization", $"Bearer {token}");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("error:");
            Debug.Log(request.error);
            //StatusText.text = "failed to update stats";
        } else {
            Debug.Log("success updating stats");
            Debug.Log(request.downloadHandler.text);
            //StatusText.text = request.downloadHandler.text;
        }
    }


}
