/*
 * Game manager script, takes care of spawning
 */

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Realtime;

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

        // Start Method
        void Start()
        {
            if (!PhotonNetwork.IsConnected) // Check if client is connected
            {
                SceneManager.LoadScene("Launcher"); // Reload Launcher to attempt reconnect
                return;
            }

            //Look into making playermanager script to get rid of extra demos
            if (PlayerManager.LocalPlayerInstance == null)
            {
                if (PhotonNetwork.IsMasterClient) // Check if client is master client
                {
                    Debug.Log("Instantiating Player 1");
                    // Instantiate first player, save reference to player1
                    player1 = PhotonNetwork.Instantiate("PlayerIndustrialist1",
                        player1SpawnPosition.transform.position,
                        player1SpawnPosition.transform.rotation, 0);
                    Debug.Log("master");
                    player1.AddComponent<Movement>();
                    minionAI = PhotonNetwork.Instantiate("EnemyAITester",
                        minionAISpawnPosition.transform.position,
                        minionAISpawnPosition.transform.rotation, 0);
                }
                else // Normal clients instantiate second player, save reference to player2
                {
                    //Add more code here for player 3 and 4
                    player2 = PhotonNetwork.Instantiate("PlayerIndustrialist2",
                        player2SpawnPosition.transform.position,
                        player2SpawnPosition.transform.rotation, 0);
                    player2.AddComponent<Movement>();
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
