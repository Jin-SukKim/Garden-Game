/*
 * Game manager script, takes care of spawning
 * Authors: Marvin, Zora
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

        public GameObject indust1SpawnPosition;
        public GameObject indust2SpawnPosition;
        public GameObject druid1SpawnPosition;
        public GameObject druid2SpawnPosition;

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
            PhotonNetwork.AutomaticallySyncScene = true;

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
                // For testing
                /*PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                        player1SpawnPosition.transform.position,
                        player1SpawnPosition.transform.rotation, 0).AddComponent<Movement>();*/

                // Hard coded spawns based on name of gameobject...
                switch ((string)properties[PhotonNetwork.LocalPlayer.NickName])
                {
                    case "MoneyMan":
                        PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                                                    indust1SpawnPosition.transform.position,
                                                    indust1SpawnPosition.transform.rotation, 0).AddComponent<Movement>();
                        break;
                    case "Suffrogette":
                        PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                                                    indust2SpawnPosition.transform.position,
                                                    indust2SpawnPosition.transform.rotation, 0).AddComponent<Movement>();
                        break;
                    case "AnimalLover":
                        PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                                                    druid1SpawnPosition.transform.position,
                                                    druid1SpawnPosition.transform.rotation, 0).AddComponent<Movement>();
                        break;
                    case "Activist":
                        PhotonNetwork.Instantiate((string)properties[PhotonNetwork.LocalPlayer.NickName],
                                                    druid2SpawnPosition.transform.position,
                                                    druid2SpawnPosition.transform.rotation, 0).AddComponent<Movement>();
                        break;
                    default:
                        Debug.LogError("Selected player does not exist in spawning swich...");
                        break;
                } 
            }
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
        }


        public void QuitRoom()
        {
            Application.Quit();
        }




    }
}
