using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{

    public Text timerText;

    public float startingTime = 60f;
    public float currentTime;
    
    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Round(currentTime) + "";
    }
}
