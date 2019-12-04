using UnityEngine;

using Photon;
using Photon.Pun;
using System.Collections.Generic;

public class InputManager : MonoBehaviourPun
{
    private Vector3 firePoint;

    private Entity MyEntity;

    //My entity components
  //  public PhotonView photonView;
    public Abilities abilities;
    public Movement movement;

    // For animations
    private List<string> animationIDs;

    void Start()
    {

        // Attached on script attachment see gamemanager
        MyEntity = gameObject.GetComponent<Entity>();
        /*photonView = PhotonView.Get(this);*/
     //   photonView = gameObject.GetComponent<PhotonView>();
        abilities = gameObject.GetComponent<Abilities>();
        movement = gameObject.GetComponent<Movement>();

        // For animations
        animationIDs = new List<string>();
        animationIDs.Add("BasicAttack");
        animationIDs.Add("Ability");
        animationIDs.Add("RallyOrPlant");
        animationIDs.Add("Ultimate");

        Debug.Log("START" + photonView);
    }

    // Due to the nature of photon we must attach this script to the object with the photonview so I've commented out this code.
    /*    public void InitializeInputManager(Entity myEntity)
        {
            MyEntity = myEntity;
            photonView = MyEntity.gameObject.GetComponent<PhotonView>();
            abilities = MyEntity.gameObject.GetComponent<Abilities>();
            movement = MyEntity.gameObject.GetComponent<Movement>();
        }*/


    // Network function for firing abilities
    [PunRPC]
    public void Fire(Vector3 pos, int abilityNum)
    {
        abilities.castAbility(abilityNum, pos);
    }

    [PunRPC]
    public void Plant(Vector3 pos)
    {
        abilities.plantAbility((int)gameObject.GetComponent<PlantingSystem>().SelectedPlant, pos);
        Destroy(MyEntity.gameObject.GetComponent<PlantingSystem>());
    }
    // Network functions for triggering animations 
    [PunRPC]
    private void triggerAnim(string animString)
    {
        GetComponentInChildren<Animator>().SetTrigger(animString);
    }
    [PunRPC]
    private void setAnimBool(string animString, bool boolean)
    {
        GetComponentInChildren<Animator>().SetBool(animString, boolean);
    }


    void Update()
    {
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

        if ((photonView.IsMine || !PhotonNetwork.IsConnected))
        {
            //// Points the player's cube object in the direction of the cursor 
            //// Ray casting to get the cursor position, uses that result to direct player
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (plane.Raycast(ray, out distance))
            {
                // Target value is the instant location of cursor, can be used for shooting function later
                firePoint = ray.GetPoint(distance);
            }

            if (!MyEntity.IsDisabled)
            {
                Vector3 direction = firePoint - MyEntity.gameObject.transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                MyEntity.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            if (MyEntity.CanCast)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    int index = 0;

                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, index);
                        triggerAnim(animationIDs[index]);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, index);
                        this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[index]);
                    }
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    int index = 1;

                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, index);
                        triggerAnim(animationIDs[index]);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, index);
                        this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[index]);
                    }
                }
                if (Input.GetButtonDown("Fire3"))
                {
                    int index = 2;

                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, index);
                        triggerAnim(animationIDs[index]);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, index);
                        this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[index]);
                    }
                }
                if (Input.GetButtonDown("Fire4"))
                {
                    int index = 3;

                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, index);
                        triggerAnim(animationIDs[index]);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, index);
                        this.photonView.RPC("triggerAnim", RpcTarget.AllBuffered, animationIDs[index]);
                    }
                }
            }

            if (MyEntity.CanMove)
            {
                movement.horizAxis = Input.GetAxis("HorizKey");
                movement.vertAxis = Input.GetAxis("VertKey");

                if (movement.horizAxis != 0 || movement.vertAxis != 0)
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        setAnimBool("isRunning", true);
                    }
                    else
                    {
                        this.photonView.RPC("setAnimBool", RpcTarget.AllBuffered, "isRunning", true);
                    }
                }
                else
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        setAnimBool("isRunning", false);
                    }
                    else
                    {
                        this.photonView.RPC("setAnimBool", RpcTarget.AllBuffered, "isRunning", false);
                    }
                }
            }
            /*
            if (MyEntity.IsPlanting)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                    Debug.Log("Plant1");
                    if (!PhotonNetwork.IsConnected)
                    {
                        Plant(firePoint);
                    }
                    else
                    {
                        this.photonView.RPC("Plant", RpcTarget.AllBuffered, firePoint);
                    }
                }
            }
            */
        }
    }

    public Vector3 GetPointerPosition()
    {
        return firePoint;
    }

}
