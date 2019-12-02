/*
 * Used to display who won the game
 * Authors: Joanna, Zora (just touched it up for integration 90% Joanna's work)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameEndSplash : MonoBehaviour
{
    public Canvas druidDefeatCanvas;
    public Canvas druidVictoryCanvas;
    public Canvas industrialistDefeatCanvas;
    public Canvas industrialistVictoryCanvas;

    public enum gameStates
    {
        druidDefeat,
        druidVictory,
        industrialistDefeat,
        industrialistVictory,
        nothing // Added as default
    };


    public gameStates myState;

    // Start is called before the first frame update
    void Start()
    {
        //get compoments
        druidDefeatCanvas = GameObject.Find("DruidDefeat").GetComponent<Canvas>();
        druidVictoryCanvas = GameObject.Find("DruidVictory").GetComponent<Canvas>();
        industrialistDefeatCanvas = GameObject.Find("IndustrialistDefeat").GetComponent<Canvas>();
        industrialistVictoryCanvas = GameObject.Find("IndustrialistVictory").GetComponent<Canvas>();

        //set initial state for testing 
        //myState = gameStates.druidDefeat; 
        ChangeState(myState);
    }

    public void ChangeState(gameStates state)
    {
        myState = state;

        //disable all Canvases
        druidDefeatCanvas.enabled = false;
        druidVictoryCanvas.enabled = false;
        industrialistDefeatCanvas.enabled = false;
        industrialistVictoryCanvas.enabled = false;

        //only enable the one we want to show
        if (myState == gameStates.druidDefeat)
        {
            druidDefeatCanvas.enabled = true;
        }
        else if (myState == gameStates.druidVictory)
        {
            druidVictoryCanvas.enabled = true;
        }
        else if (myState == gameStates.industrialistDefeat)
        {
            industrialistDefeatCanvas.enabled = true;
        }
        else if (myState == gameStates.industrialistVictory)
        {
            industrialistVictoryCanvas.enabled = true;
        } else if (myState == gameStates.nothing)
        {
            druidDefeatCanvas.enabled = false;
            druidVictoryCanvas.enabled = false;
            industrialistDefeatCanvas.enabled = false;
            industrialistVictoryCanvas.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
