using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerControl : MonoBehaviour
{
    //The text that displays the remaining time in the round.
    public Text timerText;

    //The length of the round in seconds
    public float startingTime = 60f;

    //The current time left in the round
    public float currentTime;

    //The remaining minutes left in the round.
    public int mins;
    
    //The remaining seconds left in the round( - whole mins)
    public int secs;
    
    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        mins = (int)Mathf.Round(currentTime) / 60;
        secs = (int)Mathf.Round(currentTime) % 60;
        timerText.text =  mins.ToString() + ":" + secs.ToString("00");
    }
}
