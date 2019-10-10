/*
 * Launcher script, takes care of lobbies and rooms
 */

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace Photon.Pun.Demo.PunBasics
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject controlPanel;

        [SerializeField]
        private Text feedbackText;

        [SerializeField]
        private byte maxPlayersPerRoom = 2;

        bool isConnecting;

        string gameVersion = "1";

        [Space(10)]
        [Header("Custom Variables")]
        public InputField playerNameField;
        public InputField roomNameField;

        [Space(5)]
        public Text playerStatus;
        public Text connectionStatus;

        [Space(5)]
        public GameObject roomJoinUI;
        public GameObject buttonLoadArena;
        public GameObject buttonJoinRoom;

        string playerName = "";
        string roomName = "";

        // Start Method
        void Start()
        {
            /* Delete previous player preferences to avoid connecting to high ping server
             * PUN pings available servers and stores IP address of lowest ping in PlayerPrefs
             */

            PlayerPrefs.DeleteAll();

            Debug.Log("Connecting to Photon Network");

            /*Hide UI elements until connection is established
             */
            roomJoinUI.SetActive(false);
            buttonLoadArena.SetActive(false);

            //Connect to Photon method
            ConnectToPhoton();
        }

        // Awake method
        void Awake()
        {
            //Sync scenes across all clients
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        // Set player name helper method
        public void SetPlayerName(string name)
        {
            Debug.Log(playerNameField.text);
            playerName = playerNameField.text; 
        }

        //Set room name helper method
        public void SetRoomName(string name)
        {
            roomName = roomNameField.text;
        }

        // Set game version and connect to photon network
        void ConnectToPhoton()
        {
            connectionStatus.text = "Connecting...";
            PhotonNetwork.GameVersion = gameVersion; //Version string to seperate different version clients
            PhotonNetwork.ConnectUsingSettings(); //Connects to photon 
                                                  /*If connecting fails check docs, possibly
                                                   * Invalid AppId
                                                   * Network issues
                                                   * Invalid region
                                                   * Subscription CCU limit reached
                                                   */
            //Debug.Log("connect to photon called");
        }

        // Set player name and join room based on inputs
        public void JoinRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = playerName; //NickName is unique identifier for everyone on current network
                Debug.Log("PhotonNetwork.IsConnected! | Trying to Create/Join Room " +
                    roomNameField.text);
                Debug.Log(roomName.ToString());
                RoomOptions roomOptions = new RoomOptions(); //RoomOptions object for settings, see docs for how to config
                TypedLobby typedLobby = new TypedLobby(roomName, LobbyType.Default); //TypedLobby object for lobby type (will only have one)
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby); //Creates room if no match, otherwise join
                //Debug.Log("finished join or create");
            }
        }

        // Only allows arena to be loaded once all players connected
        public void LoadArena()
        {
            // Load arena if both players connected (change to > 3 for our game)
            if (PhotonNetwork.CurrentRoom.PlayerCount > 1)
            {
                PhotonNetwork.LoadLevel("MainEnvironment");
            }
            else
            {
                playerStatus.text = "Minimum 2 Players required to Load Arena!";
            }
        }

        // Photon Methods for PUN Callbacks

        // Called when user connects to Photon Network
        public override void OnConnected()
        {
            // Signal connection established... refer to docs for more detail
            base.OnConnected();
            // Feedback on UI for connection success
            connectionStatus.text = "Connected to Photon!";
            connectionStatus.color = Color.green;
            roomJoinUI.SetActive(true);
            buttonLoadArena.SetActive(false);
            Debug.Log("connected");
        }

        // Disconnection handling
        public override void OnDisconnected(DisconnectCause cause)
        {
            isConnecting = false;
            controlPanel.SetActive(true);
            Debug.LogError("Disconnected. Please check your Internet connection.");
        }

        // Called when user joins room
        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom Called");
            // Checks if current client is master and gives power to load arena
            if (PhotonNetwork.IsMasterClient)
            {
                buttonLoadArena.SetActive(true);
                buttonJoinRoom.SetActive(false);
                playerStatus.text = "You are Lobby Leader";
            }
            else // Otherwise plebs are just connected
            {
                playerStatus.text = "Connected to Lobby";
            }
        }
    }
}
