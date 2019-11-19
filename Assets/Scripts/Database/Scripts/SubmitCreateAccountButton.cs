using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Networking;

public class SubmitCreateAccountButton : MonoBehaviour {

    private Text StatusText;

    [SerializeField]
    private GameObject CreateAccountButton;

    [SerializeField]
    private InputField UsernameInputField;

    [SerializeField]
    private InputField PasswordInputField;

    [SerializeField]
    private InputField PlayerNameInputField;

    void Start() {
        Button createAccountButton = gameObject.GetComponent<Button>();
        StatusText = GameObject.Find("statusText").GetComponent<Text>();
        UsernameInputField = GameObject.Find("UsernameInputField").GetComponent<InputField>();
        PasswordInputField = GameObject.Find("PasswordInputField").GetComponent<InputField>();
        createAccountButton.onClick.AddListener(CreateAccountButtonClick);
    }

    public void CreateAccountButtonClick() {

        ValidateInput.Validation usernameValid = ValidateInput.checkUsername(UsernameInputField.text);
        ValidateInput.Validation passwordValid = ValidateInput.checkPassword(PasswordInputField.text);

        if (usernameValid == ValidateInput.Validation.isValid &&
          passwordValid == ValidateInput.Validation.isValid) {
            StatusText.text = "creating account...";
            string username = UsernameInputField.text;
            string passwordHash = PasswordHash.Generate(PasswordInputField.text);

            string localUrl = APIEndpoint.CreateAccountLocal;
            string cloudUrl = APIEndpoint.CreateAccountCloud;

            StartCoroutine(SendHttpRequest(localUrl, username, passwordHash));
            //StartCoroutine(SendHttpRequest(cloudUrl));

        } else {
            switch (usernameValid) {
                case ValidateInput.Validation.usernameLetterDigits:
                    StatusText.text = "username can only contain letters and numbers";
                    break;
            }
        }

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
            StatusText.text = "that username is taken";
        } else {
            Account account = JsonUtility.FromJson<Account>(request.downloadHandler.text);
            PlayerPrefs.SetString("username", account.username);
            PlayerPrefs.SetString("token", account.jsonWebToken);

            Debug.Log(request.downloadHandler.text);
            StatusText.text = $"welcome {PlayerPrefs.GetString("username")}\n" +
                              $"your jwt is {PlayerPrefs.GetString("token")}";

            PlayerNameInputField.text = PlayerPrefs.GetString("username");
            UsernameInputField.text = string.Empty;
            PasswordInputField.text = string.Empty;
            CreateAccountButton.SetActive(false);
        }
    }

}