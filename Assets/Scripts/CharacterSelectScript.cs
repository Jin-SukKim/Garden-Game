using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CharacterSelectScript : MonoBehaviourPunCallbacks
{
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
        pv = gameObject.GetComponent<PhotonView>();

        PlayerID = PhotonNetwork.LocalPlayer.NickName;

        SelectButtons[0] = GameObject.Find("DruidSelect1");
        SelectButtons[1] = GameObject.Find("DruidSelect2");
        SelectButtons[2] = GameObject.Find("IndustrialistSelect1");
        SelectButtons[3] = GameObject.Find("IndustrialistSelect2");
    }

    public void ClickSelect(int i)
    {
            if (PlayerSelection >= 0) deselectChar(PlayerSelection);

            PlayerSelection = i;
            selectChar(PlayerSelection);
    }

    [PunRPC]
    private void selectChar(int i)
    {
        Button but = SelectButtons[i].GetComponent<Button>();

        ColorBlock cb = but.colors;
        cb.normalColor = selected;
        cb.highlightedColor = selected;
        but.colors = cb;

        Text t = SelectButtons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
        t.text = PlayerID;
    }

    [PunRPC]
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
}
