using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LobbyRedirect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendToLobby()
    {
        PlayerPrefs.SetString("guest", "true");
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("token");

        SceneManager.LoadScene("Lobby");
    }
}
