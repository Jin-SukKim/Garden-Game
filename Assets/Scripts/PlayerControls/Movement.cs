using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    //public GunController theGun;

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


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PHOTON VIEW: " + photonView);
        // Link the animator
        //anim = GetComponent<Animator>();


        rb = GetComponent<Rigidbody2D>();

        // Establish the forwardBack direction relative to the camera's
        forwardBackDirection = Camera.main.transform.forward;

        // Ensure the y value is always zero since we operate in the X Z axis only. 
        forwardBackDirection.y = 0;

        // Normalizing a vector keeps a vectors direction but sets the length to 1.
        // basically allows us to use the vector for motion.
        forwardBackDirection = Vector3.Normalize(forwardBackDirection);

        // Quaternion creates a rotation for the leftRight vector, telling it to rotate 90 degrees around the x axis 
        // leftRightDirection = Quaternion.Euler(new Vector3(0, 90, 0)) * forwardBackDirection;

        // Establish the leftRight direction relative to the camera's
        leftRightDirection = Camera.main.transform.right;

        photonView = gameObject.GetComponent<PhotonView>();
        //playerName = photonView.Owner.NickName;
        gameObject.name = playerName;

        // Attach camera to player through a reference to the camera objects script
        CameraWorks camWorks = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraWorks>();
        camWorks.target = gameObject.transform;
        Debug.Log("THIS IS THE PLAYER THAT HAS MOVEMENT: " + gameObject.name);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine || !PhotonNetwork.IsConnected){
            // WASD movement

            // Triggering animations through parameters
/*            if (Input.GetAxis("HorizKey") != 0 || Input.GetAxis("VertKey") != 0)
            {
                // Set walking to true if there is a movement input
                anim.setbool("iswalking", true);
                Debug.Log("MOVING!");
            }
            else
            {
                anim.SetBool("IsWalking", false);
                Debug.Log("NOT MOVING!");
            }*/

            // Using Time.deltaTime allows for smoother movement
            Vector3 leftRightMovement = leftRightDirection * speed * Time.deltaTime * Input.GetAxis("HorizKey");
            Vector3 forwardBackMovement = forwardBackDirection * speed * Time.deltaTime * Input.GetAxis("VertKey");

            // Causes movement
            transform.position += leftRightMovement;
            transform.position += forwardBackMovement;
        }
    }
}
