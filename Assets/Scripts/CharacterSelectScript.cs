using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectScript : MonoBehaviour
{
    const string PlayerID = "Player1";

    int PlayerSelection = -1;
    GameObject[] SelectButtons = new GameObject[4];

    Color unSelected = new Color(0, 0, 0, 255);
    Color selected;

    // Start is called before the first frame update
    void Start()
    {
        selected = new Color(50, 50, 50, 255);

        SelectButtons[0] = GameObject.Find("DruidSelect1");
        SelectButtons[1] = GameObject.Find("DruidSelect2");
        SelectButtons[2] = GameObject.Find("IndustrialistSelect1");
        SelectButtons[3] = GameObject.Find("IndustrialistSelect2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ClickSelect(int i)
    {
        PlayerSelection = i;
        print(i);
        Button but = SelectButtons[i].GetComponent<Button>();

        ColorBlock cb = but.colors;
        cb.normalColor = selected;
        but.colors = cb;
        print(cb.ToString());
    }
}
