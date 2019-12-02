using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;

public class ScrollViewAdapter: MonoBehaviour
{

    //list of all RootObjects for one pop up canvas
    public List<LeaderboardObject> slideList = new List<LeaderboardObject>();
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.ToString();
    }

    public void UpdateItems()
    {

    }

    // called from somewhere else?
    public void postReq(string str1, string str2)
    {
        StartCoroutine(PostRequest(/* do we need parameters here?*/));
    }

    /* PostRequest:
     * Gets all items from database by name as a json object, saves the data members in RootObjects, and updates slideList list
     */
    public IEnumerator PostRequest(/*string fieldName1, string fieldValue1*/)
    {
        WWWForm form = new WWWForm();
        //form.AddField(fieldName1, fieldValue1);

        using (UnityWebRequest www = UnityWebRequest.Post(""/*needs to be our project db string */, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                yield return null;
            }
            else
            {
                slideList.Clear();

                Debug.Log(":\nReceived: " + www.downloadHandler.text);

                //Deserialize JSON 
                var parsedJSON = JSON.Parse(www.downloadHandler.text);
                var id = parsedJSON[3]["id"].Value;
                
                int i = 0;
                //don't know how to get the total list size so just check if the next entry is an empty string
                //might wanna change json structure later
                while (!(parsedJSON[i]["id"].Value.Equals("")))
                {
                    LeaderboardObject leadObj = new LeaderboardObject();

                    // whatever the actual values are coming from the db
                    leadObj.user = parsedJSON[i]["user"].Value;
                    leadObj.score = parsedJSON[i]["score"].Value;
                }
            }

            foreach (var listItem in slideList)
            {
                GameObject instance = Instantiate(myPrefab);
                instance.GetComponentInChildren<Text>().text = "jfjh";
                instance.transform.SetParent(gameObject.transform, false);
            }
        }
    }
}
