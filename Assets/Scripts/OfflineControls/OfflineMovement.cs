using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineMovement : MonoBehaviour {
    private Rigidbody2D rb;
    public OfflineGunController theGun;

    [SerializeField]
    float speed = 4f;

    // Since the map is placed diagonally, we cannot use the actual X Z values of the terrain,
    // we must make our own direction variables for the direction the player would assume the
    // character should move.
    Vector3 forwardBackDirection, leftRightDirection;


    // Start is called before the first frame update
    void Start() {
        theGun = GameObject.Find("OfflineGun").GetComponent<OfflineGunController>();

        rb = gameObject.GetComponent<Rigidbody2D>();

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

        // Attach camera to player through a reference to the camera objects script
        CameraWorks camWorks = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraWorks>();
        camWorks.target = gameObject.transform;
        Debug.Log("CALLED START IN OFFLINE MOVE");

    }

    // Update is called once per frame
    void Update() {
        //// Points the player's cube object in the direction of the cursor
        //// Ray casting to get the cursor position, uses that result to direct player
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;

        if (plane.Raycast(ray, out distance)) {

            // Target value is the instant location of cursor, can be used for shooting function later
            Vector3 target = ray.GetPoint(distance);

            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }

        // WASD movement
        // Using Time.deltaTime allows for smoother movement
        Vector3 leftRightMovement = leftRightDirection * speed * Time.deltaTime * Input.GetAxis("HorizKey");
        Vector3 forwardBackMovement = forwardBackDirection * speed * Time.deltaTime * Input.GetAxis("VertKey");

        if (Input.GetAxis("HorizKey") > 0) {
            Debug.Log("horiz");
        }

        if (Input.GetAxis("VertKey") > 0) {
            Debug.Log("vert");
        }


        // Get the combined 
        //Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        // Causes rotation
        //transform.forward = heading;

        // Causes movement
        transform.position += leftRightMovement;
        transform.position += forwardBackMovement;


        //shooting mechanics
        if (Input.GetMouseButtonDown(0)) {
            theGun.isFiring = true;
        }

        if (Input.GetMouseButtonUp(0)) {
            theGun.isFiring = false;
        }
    }
}
