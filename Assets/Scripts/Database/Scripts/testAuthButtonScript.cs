using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class testAuthButtonScript : MonoBehaviour {

    private Text StatusText;

    void Start() {
        Button testButton = gameObject.GetComponent<Button>();
        StatusText = GameObject.Find("statusText").GetComponent<Text>();
        testButton.onClick.AddListener(test);
    }

    public void test() {
        StatusText.text = "checking if authorized...";

        string username = PlayerPrefs.GetString("username", string.Empty);
        Debug.Log(username);

        if (username == string.Empty) {
            StatusText.text = "not logged in";
        }

        string localurl = $"localhost:54882/api/test?username={username}";
        string cloudUrl =
            $"https://techpro2019.azurewebsites.net/api/test?username={username}";

        StartCoroutine(GetRequest(localurl));
        //StartCoroutine(GetRequest(cloudUrl));
    }

    IEnumerator GetRequest(string uri) {

        UnityWebRequest www = UnityWebRequest.Get(uri);
        string token = PlayerPrefs.GetString("token", string.Empty);
        if (token == string.Empty) {
            StatusText.text = "not logged in";
        }
        www.SetRequestHeader("Authorization", $"Bearer {token}");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            StatusText.text = "not authorized";
            Debug.Log(www.error);
        } else {
            Debug.Log(www.downloadHandler.text);
            StatusText.text = "authorized";
        }

        PlayerPrefs.SetString("username", string.Empty);
        PlayerPrefs.SetString("token", string.Empty);
    }

}