/*
 * time manager script, takes care of tracking time elapsed and watching for win conditions
 * Authors: Joe, Zora
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class TimeManager : MonoBehaviourPun
{
    // The lifetree
    public GameObject lifeTree;

    // To track time
    private int seconds;
    private int mins;
    private int secs;

    // Trigger win conditions
    public bool druidWin;
    public bool industWin;

    Text timerText;

    ExitGames.Client.Photon.Hashtable properties;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("STARTING TIME SCRIPT");
        // Attach nexus
        lifeTree = GameObject.Find("Nexus");

        timerText = GameObject.Find("TimerText").GetComponent<Text>();

        // START GAME TIMER and set win conditions to false
        druidWin = false;
        industWin = false;
        seconds = 100;
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        properties = PhotonNetwork.CurrentRoom.CustomProperties;

        // Check for win conditions
        if (industWin == true)
        {
            Debug.Log("GAME ENDED WIN CONDITION MET LOADING SPASH");

            // add who won to hashtable to persist scene switch
            this.photonView.RPC("addProp", RpcTarget.AllBuffered, "Indust");
            PhotonNetwork.LoadLevel("GameEndSplash");
            return;
        }
        else if (druidWin == true)
        {
            Debug.Log("GAME ENDED WIN CONDITION MET LOADING SPASH");

            // add who won to hashtable to persist scene switch
            this.photonView.RPC("addProp", RpcTarget.AllBuffered, "Druid");
            PhotonNetwork.LoadLevel("GameEndSplash");
            return;
        }

        mins = seconds / 60;
        secs = seconds % 60;
        timerText.text = mins + ":" + secs.ToString("00");
    }

    [PunRPC]
    private void addProp(string winner)
    {
        if(properties["winner"] == null)
        {
            properties.Add("winner", winner);
        }
    }

    // Tracks time
/*    private IEnumerator roundStartTimer()
    {

        Debug.Log("In round start timer");
        while (true)
        {
            roundStartSeconds--;

            // trigger decrement
            yield return new WaitForSeconds(1.0f);
        }
    }*/

   
    // Tracks time
    private IEnumerator timer()
    {

        Debug.Log("In timer");
        while (true)
        {
            Debug.Log("WHILE RUNNING");
            // If life tree is killed INDUST WIN
            if (lifeTree == null)
            {
                Debug.Log("INDUSTRALISTS KILLED TREE");
                industWin = true;
                // End timer
                StopCoroutine(timer());
                break;
            }

            // If timer runs out and life tree is alive DRUID WIN
            if (seconds <= 0 && lifeTree != null)
            {
                Debug.Log("DRUIDS HAVE DEFENDED THE TREE");
                druidWin = true;
                // End timer
                StopCoroutine(timer());
                break;
            }
            seconds--;

            // trigger decrement
            yield return new WaitForSeconds(1.0f);
        }
    }
}
