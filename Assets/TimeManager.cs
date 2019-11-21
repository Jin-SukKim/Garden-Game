using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // The lifetree
    public GameObject lifeTree;

    // To track time
    private int seconds;

    // Trigger win conditions
    public bool druidWin;
    public bool industWin;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("STARTING TIME SCRIPT");
        // Attach nexus
        lifeTree = GameObject.FindGameObjectWithTag("Nexus");

        // START GAME TIMER and set win conditions to false
        druidWin = false;
        industWin = false;
        seconds = 10;
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        // Check for win conditions
        if (industWin == true)
        {
            Debug.Log("Industrialists win! Druids lose!");
            // Do something to reflect win on indust clients
            // Do something to reflect loss on druid client 
        }
        if (druidWin == true)
        {
            Debug.Log("Druids win! Industrialists lose!");
            // Do something to reflect win on druid clients
            // Do something to reflect loss on indust clients
        }
    }

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
