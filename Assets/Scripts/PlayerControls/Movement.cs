using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 4f;

    // Since the map is placed diagonally, we cannot use the actual X Z values of the terrain,
    // we must make our own direction variables for the direction the player would assume the
    // character should move.
    Vector3 forwardBackDirection, leftRightDirection;

    public PhotonView photonView;

    private string playerName = "";
    // Will be used later for animation
    //private Animator anim;

    public float horizAxis;
    public float vertAxis;
    private Animator animationController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PHOTON VIEW: " + photonView);
        // Link the animator, will be used later
        //anim = GetComponent<Animator>();

        // attach photon view WITH THE GAMEOBJECT KEYWORD :)
        photonView = gameObject.GetComponent<PhotonView>();

        // Establish the forwardBack direction relative to the camera's
        forwardBackDirection = Camera.main.transform.forward;

        // Ensure the y value is always zero since we operate in the X Z axis only. 
        forwardBackDirection.y = 0;

        // Normalizing a vector keeps a vectors direction but sets the length to 1.
        forwardBackDirection = Vector3.Normalize(forwardBackDirection);

        // Establish the leftRight direction relative to the camera's
        leftRightDirection = Camera.main.transform.right;

        //playerName = photonView.Owner.NickName;
        gameObject.name = playerName;

        // Attach camera to player through a reference to the camera objects script
        CameraWorks camWorks = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraWorks>();
        camWorks.target = gameObject.transform;
        Debug.Log("THIS IS THE PLAYER THAT HAS MOVEMENT: " + gameObject.name);

        animationController = gameObject.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected){
            // WASD movement

            // Using Time.deltaTime allows for smoother movement
            Vector3 leftRightMovement = leftRightDirection * speed * Time.deltaTime * horizAxis;
            Vector3 forwardBackMovement = forwardBackDirection * speed * Time.deltaTime * vertAxis;

            // Causes movement
            transform.position += leftRightMovement;
            transform.position += forwardBackMovement;

            if (horizAxis != 0 || vertAxis != 0)
                animationController.SetBool("isRunning", true);
            else
                animationController.SetBool("isRunning", false);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
