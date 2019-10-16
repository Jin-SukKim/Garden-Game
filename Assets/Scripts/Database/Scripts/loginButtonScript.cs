using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Networking;

public class loginButtonScript : MonoBehaviour {

    private Text StatusText;
    [SerializeField]

    GameObject createAccountButton;

    private InputField UsernameInputField;
    private InputField PasswordInputField;

    [SerializeField]
    InputField playerNameInputField;

    void Start() {
        Button loginButton = gameObject.GetComponent<Button>();
        StatusText = GameObject.Find("statusText").GetComponent<Text>();
        UsernameInputField = GameObject.Find("usernameInputField").GetComponent<InputField>();
        PasswordInputField = GameObject.Find("passwordInputField").GetComponent<InputField>();
        loginButton.onClick.AddListener(test);
    }

    public void test() {
        StatusText.text = "logging in...";
        string username = UsernameInputField.text;
        string password = PasswordInputField.text;

        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
        Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string pwHash = Convert.ToBase64String(hashBytes);
        //Debug.Log(pwHash);

        PlayerPrefs.SetString("username", username);
        string localurl = $"localhost:54882/api/account/create";
        string cloudUrl = $"https://techpro2019.azurewebsites.net/api/login/{username}";

        StartCoroutine(GetRequest(localurl, username, pwHash));
        //StartCoroutine(GetRequest(cloudUrl));
    }

    IEnumerator GetRequest(string uri, string username, string pwHash) {
        WWWForm formData = new WWWForm();
        formData.AddField("username", username);
        formData.AddField("pwHash", pwHash);

        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("test", "test data"));
        //formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));

        //Account account = new Account();
        //account.username = "test";
        //string bodyData = JsonUtility.ToJson(account.username);
        //Debug.Log(bodyData);


        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        www.SetRequestHeader("Content-Type", "text/plain");
        //www.SetRequestHeader("Accept", "text/csv");
        //www.SetRequestHeader("content-Type", "text/plain");
        //www.SetRequestHeader("content-Type", "application/json");
        //www.SetRequestHeader("Accept", "application/json");
        //www.SetRequestHeader("api-version", "0.1");

        //UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
            StatusText.text = "failed to login";
        } else {
            Debug.Log(www.downloadHandler.text);
            Account account = JsonUtility.FromJson<Account>(www.downloadHandler.text);
            PlayerPrefs.SetString("token", account.jsonWebToken);
            StatusText.text = $"welcome {PlayerPrefs.GetString("username")}\n" +
                              $"your token is {PlayerPrefs.GetString("token")}";
            playerNameInputField.text = PlayerPrefs.GetString("username");
            createAccountButton.SetActive(false);
        }

    }

}