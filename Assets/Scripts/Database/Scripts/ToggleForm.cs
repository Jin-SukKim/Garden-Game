using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameObject = UnityEngine.GameObject;

public class ToggleForm : MonoBehaviour {

    [SerializeField]
    private Button ClickToDoSomethingButton;

    [SerializeField]
    private GameObject SubmitFormButton;

    void Start() {
        ClickToDoSomethingButton.onClick.AddListener(ToggleFormVisibility);
        SubmitFormButton.SetActive(false);
    }

    public void ToggleFormVisibility() {
        if (SubmitFormButton.activeInHierarchy) {
            SubmitFormButton.SetActive(false);
        } else {
            SubmitFormButton.SetActive(true);
        }
    }

}
