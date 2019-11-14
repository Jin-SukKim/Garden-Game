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
    public string roomSize;

    public InputField roomNameField;
    public InputField roomSizeField;
    public GameObject roomListingPrefab;
    public Transform roomsPanel;

    public List<RoomInfo> roomListings;

    public void Awake()
    {
        lobby = this; //Creates singleton
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        PhotonNetwork.ConnectUsingSettings();
        roomListings = new List<RoomInfo>();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000);
        JoinLobby();
    }

    //List<RoomInfo> only gives the changes that were made on room list update
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        //RemoveRoomListings();

        int tempIndex;
        foreach (RoomInfo room in roomList)
        {
            if(roomListings != null)
            {
                tempIndex = roomListings.FindIndex(ByName(room.Name));
            }
            else
            {
                tempIndex = -1;
            }
            if(tempIndex != -1 || room.PlayerCount == 0)
            {
                roomListings.RemoveAt(tempIndex);
                Destroy(roomsPanel.GetChild(tempIndex).gameObject);
            }
            else
            {
                roomListings.Add(room);
                ListRoom(room);
            }
            Debug.Log(room.Name);
        }
    }

    static System.Predicate<RoomInfo> ByName(string name)
    {
        return delegate (RoomInfo room)
        {
            return room.Name == name;
        };
    }

    public void RemoveRoomListings()
    {
        for(int i = 0; i < roomsPanel.childCount; ++i)
        {
            Destroy(roomsPanel.GetChild(i).gameObject);
        }
    }

    public void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsPanel);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.roomName = room.Name;
            tempButton.roomSize = room.PlayerCount.ToString() + "/" + room.MaxPlayers.ToString();
            tempButton.SetRoom();
        }
    }

    public void CreateRoom()
    {
        Debug.Log("Trying to create room");

        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = byte.Parse(roomSize) };
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
        roomSize = roomSizeField.text;
    }

    public void JoinLobby()
    {
        if(!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //public override void OnJoinedLobby()
    //{
    //    base.OnJoinedLobby();
    //    RemoveRoomListings();
    //    foreach (RoomInfo room in PhotonNetwork.roomList)
    //    {
    //        ListRoom(room);
    //    }
    //}
    //// Update is called once per frame
    void Update()
    {
        
    }
}
