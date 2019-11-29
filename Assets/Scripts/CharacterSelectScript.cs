using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CharacterSelectScript : MonoBehaviour
{
    public GameObject startButton;
    public PhotonView pv;
    string PlayerID;

    Color selected = new Color(0.2f, 0.2f, 0.2f);
    Color unSelectedNormal = new Color(1f, 1f, 1f);
    Color unSelectedHilight = new Color(0.75f, 0.75f, 0.75f);

    int PlayerSelection = -1;
    GameObject[] SelectButtons = new GameObject[4];

    // Start is called before the first frame update
    void Start()
    {

        //pv = gameObject.GetComponent<PhotonView>();
        if (!PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(false);
        }

        PlayerID = PhotonNetwork.LocalPlayer.NickName;

        SelectButtons[0] = GameObject.Find("DruidSelect1");
        SelectButtons[1] = GameObject.Find("DruidSelect2");
        SelectButtons[2] = GameObject.Find("IndustrialistSelect1");
        SelectButtons[3] = GameObject.Find("IndustrialistSelect2");
    }

    public void ClickSelect(int i)
    {
        Debug.Log(i);
        //Debug.Log(pv);
        byte evCodeDeSelect = 1;
        byte evCodeSelect = 2;
        object[] content = new object[] { PlayerSelection, PlayerID };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };
        string t = SelectButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>().text;
        Debug.Log("t = " + t);
        if (PlayerSelection == i)
        {
            PhotonNetwork.RaiseEvent(evCodeDeSelect, content, raiseEventOptions, sendOptions);
            PlayerSelection = -1;

        }
        else if(t.Equals(""))
        {
            if (PlayerSelection >= 0)
            {
                PhotonNetwork.RaiseEvent(evCodeDeSelect, content, raiseEventOptions, sendOptions);
            }
            PlayerSelection = i;
            content = new object[] { PlayerSelection, PlayerID };
            PhotonNetwork.RaiseEvent(evCodeSelect, content, raiseEventOptions, sendOptions);
        }
        
    }
    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        object[] data = (object[])photonEvent.CustomData;

        if (eventCode == 1)
        {
            Debug.Log("deselecting");
            deselectChar((int)data[0]);
        }
        if (eventCode == 2)
        {
            Debug.Log("selecting");
            selectChar((int)data[0], (string)data[1]);
        }
    }

    private void selectChar(int i, string playerName)
    {
        Button but = SelectButtons[i].GetComponent<Button>();

        ColorBlock cb = but.colors;
        cb.normalColor = selected;
        cb.highlightedColor = selected;
        but.colors = cb;

        Text t = SelectButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
        t.text = playerName;
        string character = "";
        switch (i)
        {
            case 0:
                character = "AnimalLover";
                break;
            case 1:
                character = "Activist";
                break;
            case 2:
                character = "MoneyMan";
                break;
            case 3:
                character = "Suffrogette";
                break;
            default:
                break;
        }
        Player p = PhotonNetwork.LocalPlayer;
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("selectedCharacter", character);
        p.SetCustomProperties(properties);
        Debug.Log("selected char = " + p.CustomProperties["selectedCharacter"]);
    }

    private void deselectChar(int i)
    {
        Button but = SelectButtons[i].GetComponent<Button>();

        ColorBlock cb = but.colors;
        cb.normalColor = unSelectedNormal;
        cb.highlightedColor = unSelectedHilight;
        but.colors = cb;

        Text t = SelectButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
        t.text = "";
    }

    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel("MainEnvironment");
    }
}
