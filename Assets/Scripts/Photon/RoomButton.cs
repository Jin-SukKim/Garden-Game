using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public Text nameText;
    public Text sizeText;

    public string roomName;
    public string roomSize;

    public void SetRoom()
    {
        nameText.text = roomName;
        sizeText.text = roomSize;
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
