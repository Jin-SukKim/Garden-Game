using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;

public class ToggleLoginFormScript : MonoBehaviour {



    [SerializeField]
    GameObject createAccountButton;
    //Button createAccountButton = GameObject.FindGameObjectWithTag("CreateAccountButton").GetComponent<Button>();
    [SerializeField]
    InputField usernameInputField;
    // Start is called before the first frame update

    [SerializeField]
    Button loginButton;

    void Start() {
        Debug.Log(createAccountButton);
        Debug.Log(usernameInputField);
        //loginButton.SetActive(false);
        loginButton.onClick.AddListener(test);
        createAccountButton.SetActive(false);
    }

    public void test() {
        if (createAccountButton.activeInHierarchy)
        {
        createAccountButton.SetActive(false);

        } else
        {
        createAccountButton.SetActive(true);

        }
        Debug.Log("a");
    }

    }
