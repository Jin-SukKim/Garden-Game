/*
 * Game manager script, takes care of spawning
 */

using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Figure out why we use demo.punbasics
namespace Photon.Pun.Demo.PunBasics
{
    public class GameManager : MonoBehaviourPunCallbacks
    {
        public GameObject player1SpawnPosition;
        public GameObject player2SpawnPosition;
        public GameObject minionAISpawnPosition;

        private GameObject player1;
        private GameObject player2;
        private GameObject minionAI;
        private List<Player> players;

        /*RESOURCE REF*/
        [SerializeField]
        private string player1ResourceString;
        [SerializeField]
        private string player2ResourceString;

        ExitGames.Client.Photon.Hashtable properties;



        // Start Method
        void Start()
        {
            
            if (!PhotonNetwork.IsConnected) // Check if client is connected
            {
                SceneManager.LoadScene("Launcher"); // Reload Launcher to attempt reconnect
                return;
            }
            properties = PhotonNetwork.CurrentRoom.CustomProperties;

            players = new List<Player>();
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
            {
                players.Add(PhotonNetwork.PlayerList[i]);
                //Below prints character name belonging to player
                //Debug.Log(properties[players[i].NickName]);
            }
            if (PlayerManager.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                        player1SpawnPosition.transform.position,
                        player1SpawnPosition.transform.rotation, 0).AddComponent<Movement>();
               
                //if (PhotonNetwork.IsMasterClient) // Check if client is master client
                //{
                //    Debug.Log("Instantiating Player 1");
                //    // Instantiate first player, save reference to player1
                //    player1 = PhotonNetwork.Instantiate(player1ResourceString,
                //        player1SpawnPosition.transform.position,
                //        player1SpawnPosition.transform.rotation, 0);
                //    Debug.Log("master");
                //    // Hookup controls
                //    player1.AddComponent<Movement>();
                //    /*player1.AddComponent<InputManager>();*/
                //    player1.tag = "Player";
                //    minionAI = PhotonNetwork.Instantiate("EnemyAITester",
                //        minionAISpawnPosition.transform.position,
                //        minionAISpawnPosition.transform.rotation, 0);
                //}
                //else // Normal clients instantiate second player, save reference to player2
                //{
                //    //Add more code here for player 3 and 4
                //    player2 = PhotonNetwork.Instantiate(player2ResourceString,
                //        player2SpawnPosition.transform.position,
                //        player2SpawnPosition.transform.rotation, 0);
                //    // Hookup controls
                //    player2.AddComponent<Movement>();
                //    /*player2.AddComponent<InputManager>();*/
                //    player2.tag = "Player";
                //}
            }
            //Finds the first player and assigns it to the input manager
            /* GameObject.Find("InputManager").GetComponent<InputManager>().InitializeInputManager(GameObject.FindWithTag("Player").GetComponent<Entity>());*/
        }

        //Quit app if escape pressed
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        // Photon callback to handle player leaving room
        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects
            if (PhotonNetwork.IsMasterClient)
            {
                //PhotonNetwork.LoadLevel("Launcher");
            }
        }


        public void QuitRoom()
        {
            Application.Quit();
        }




    }
}
