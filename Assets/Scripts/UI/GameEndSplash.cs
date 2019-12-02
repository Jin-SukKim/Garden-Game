/*
 * Used to display who won the game
 * Authors: Joanna, Zora (just touched it up for integration 90% Joanna's work)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class GameEndSplash : MonoBehaviourPun
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
    ExitGames.Client.Photon.Hashtable properties;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("IN START FOR SPLASH SCREEN");
        //get compoments
/*        druidDefeatCanvas = GameObject.Find("DruidDefeat").GetComponent<Canvas>();
        druidVictoryCanvas = GameObject.Find("DruidVictory").GetComponent<Canvas>();
        industrialistDefeatCanvas = GameObject.Find("IndustrialistDefeat").GetComponent<Canvas>();
        industrialistVictoryCanvas = GameObject.Find("IndustrialistVictory").GetComponent<Canvas>();*/
        properties = PhotonNetwork.CurrentRoom.CustomProperties;

        // Process the properties from gameMan
        switch ((string)properties[PhotonNetwork.LocalPlayer.NickName])
        {
            case "MoneyMan":
                if((string)properties["winner"] == "Indust")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.industrialistVictory);
                    ChangeState(gameStates.industrialistVictory);
                } else if ((string)properties["winner"] == "Druid")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.industrialistDefeat);
                    ChangeState(gameStates.industrialistDefeat);
                }
                break;
            case "Suffrogette":
                if ((string)properties["winner"] == "Indust")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.industrialistVictory);
                    ChangeState(gameStates.industrialistVictory);
                }
                else if ((string)properties["winner"] == "Druid")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.industrialistDefeat);
                    ChangeState(gameStates.industrialistDefeat);
                }
                break;
            case "AnimalLover":
                if ((string)properties["winner"] == "Indust")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.druidDefeat);
                    ChangeState(gameStates.druidDefeat);
                }
                else if ((string)properties["winner"] == "Druid")
                {
                    Debug.Log("Hi1");
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.druidVictory);
                    ChangeState(gameStates.druidVictory);
                    Debug.Log("hi2");
                }
                break;
            case "Activist":
                if ((string)properties["winner"] == "Indust")
                {
                   
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.druidDefeat);
                    ChangeState(gameStates.druidDefeat);
                    
                }
                else if ((string)properties["winner"] == "Druid")
                {
                    //this.photonView.RPC("ChangeState", RpcTarget.AllBuffered, gameStates.druidVictory);
                    ChangeState(gameStates.druidVictory);
                }
                break;
            default:
                break;
        }
        
        
    }

    [PunRPC]
    public void ChangeState(gameStates state)
    {
        Debug.Log("CALLING CHANGE STATe");
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
            Debug.Log("in nothing");
            druidDefeatCanvas.enabled = false;
            druidVictoryCanvas.enabled = false;
            industrialistDefeatCanvas.enabled = false;
            industrialistVictoryCanvas.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
