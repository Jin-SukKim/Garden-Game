using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobbyCustomMatch : MonoBehaviourPunCallbacks, ILobbyCallbacks
{
    public static PhotonLobbyCustomMatch lobby;

    public string roomName;
    public int roomSize;

    public InputField roomNameField;
    public InputField roomSizeField;
    public GameObject roomListingPrefab;
    public Transform roomsPanel;

    public void Awake()
    {
        lobby = this; //Creates singleton
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        RemoveRoomListings();
        foreach(RoomInfo room in roomList)
        {
            ListRoom(room);
        }
    }

    void RemoveRoomListings()
    {
        for(int i = 0; i < roomsPanel.childCount; ++i)
        {
            Destroy(roomsPanel.GetChild(i).gameObject);
        }
        //while(roomsPanel.childCount != 0)
        //{
        //    Destroy(roomsPanel.GetChild(0).gameObject);
        //}
    }

    void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.MaxPlayers;
            tempButton.SetRoom();
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Trying to create room");

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed creating new room. Room with same name exists.");
        //CreateRoom(); //Retry with different room name (random number)
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = roomNameField.text;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(roomSizeField.text);
    }

    public void JoinLobbyOnClick()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
