using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SubmitLoginButton : MonoBehaviour {

    private Text StatusText;

    [SerializeField]
    private GameObject LoginButton;

    [SerializeField]
    private InputField UsernameInputField;

    [SerializeField]
    private InputField PasswordInputField;

    [SerializeField]
    private InputField PlayerNameInputField;

    void Start() {
        Button loginButton = gameObject.GetComponent<Button>();
        StatusText = GameObject.Find("statusText").GetComponent<Text>();
        UsernameInputField = GameObject.Find("UsernameInputField").GetComponent<InputField>();
        PasswordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();
        loginButton.onClick.AddListener(LoginButtonClick);
    }

    public void LoginButtonClick() {
        StatusText.text = "logging in...";

        string username = UsernameInputField.text;
        string passwordHash = PasswordHash.Generate(PasswordInputField.text);

        string localUrl = string.Format(APIEndpoint.LoginLocal, username, passwordHash);
        string cloudUrl = string.Format(APIEndpoint.LoginCloud, UsernameInputField.text);

        StartCoroutine(SendHttpRequest(localUrl, username, passwordHash));
        //StartCoroutine(SendHttpRequest(cloudUrl));
    }

    IEnumerator SendHttpRequest(string url, string username, string passwordHash) {
        WWWForm postData = new WWWForm();
        postData.AddField("username", username);
        postData.AddField("passwordHash", passwordHash);

        UnityWebRequest request = UnityWebRequest.Post(url, postData);
        request.SetRequestHeader("Content-Type", "text/plain");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) {
            Debug.Log("error:");
            Debug.Log(request.error);
            StatusText.text = "incorrect username or password";
        } else {
            Account account = JsonUtility.FromJson<Account>(request.downloadHandler.text);

            if (PasswordHash.Validate(PasswordInputField.text, passwordHash)) {
                PlayerPrefs.SetString("username", account.username);
                PlayerPrefs.SetString("token", account.jsonWebToken);

                StatusText.text = $"welcome {PlayerPrefs.GetString("username")}\n" +
                                  $"your jwt is {PlayerPrefs.GetString("token")}";

                PlayerNameInputField.text = PlayerPrefs.GetString("username");
                UsernameInputField.text = string.Empty;
                PasswordInputField.text = string.Empty;
                LoginButton.SetActive(false);
            }
        }
    }

}