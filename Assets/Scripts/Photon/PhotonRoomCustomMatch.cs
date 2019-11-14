using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonRoomCustomMatch : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    //Room info
    public static PhotonRoomCustomMatch room;
    private PhotonView photonView;  

    public bool isGameLoaded;
    public int currentScene;

    //Player info
    Player[] photonPlayers;
    public int playersInRoom;
    public int myNumberInRoom;

    public int playersInGame;

    //Delayed start
    private bool readyToCount;
    private bool readyToStart;
    public float startingTime;
    private float lessThanMaxPlayers;
    private float atMaxPlayers;
    private float timeToStart;

    public GameObject lobbyGameObject;
    public GameObject roomGameObject;
    public Transform playersPanel;
    public GameObject playerListingPrefab;
    public GameObject roomListingPrefab;
    public GameObject startButton;

    private void Awake()
    {
        //Set up singleton
        if(PhotonRoomCustomMatch.room == null)
        {
            PhotonRoomCustomMatch.room = this;
        }
        else
        {
            if(PhotonRoomCustomMatch.room != this)
            {
                Destroy(PhotonRoomCustomMatch.room.gameObject);
                PhotonRoomCustomMatch.room = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public override void OnEnable()
    {
        //subscribe to functions
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        //SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        //SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("We are now in a room");

        lobbyGameObject.SetActive(false);
        roomGameObject.SetActive(true);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        ClearPlayerListings();
        ListPlayers();

        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom = photonPlayers.Length;
        myNumberInRoom = playersInRoom;
        //StartGame();
    }

    void ClearPlayerListings()
    {
        for(int i = playersPanel.childCount -1; i >= 0; i--)
        {
            Destroy(playersPanel.GetChild(i).gameObject);
        }
    }
    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach(Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("A new challenger has approached...");
        ClearPlayerListings();
        ListPlayers();
        photonPlayers = PhotonNetwork.PlayerList;
        playersInRoom++;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.NickName + " has left the game");
        playersInRoom--;

        ClearPlayerListings();
        ListPlayers();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }

    }

    //public override void OnLeftRoom()
    //{
    //    base.OnLeftRoom();
    //    if (playersInRoom == 0)
    //    {
    //        Debug.Log("destroying room listing prefab");

    //        Destroy(roomListingPrefab);
    //    }
    //}

    public void StartGame()
    {
        isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel("MainEnvironment");
    }   

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        lobbyGameObject.SetActive(true);
        roomGameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
