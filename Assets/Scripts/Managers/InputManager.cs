using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    // can be used to open setting windows
        //    return;
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GetComponent<AudioSource>().volume += 1.0f;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GetComponent<AudioSource>().volume -= 1.0f;
            return;
        }
    }
}
