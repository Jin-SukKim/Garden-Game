using UnityEngine;

using Photon;
using Photon.Pun;

public class InputManager : MonoBehaviourPun
{
    private Vector3 firePoint;

    private Entity MyEntity;

    //My entity components
  //  public PhotonView photonView;
    public Abilities abilities;
    public Movement movement;
    public PlantLoadout plantLoadout;
    void Start()
    {

        // Attached on script attachment see gamemanager
        MyEntity = gameObject.GetComponent<Entity>();
        /*photonView = PhotonView.Get(this);*/
     //   photonView = gameObject.GetComponent<PhotonView>();
        abilities = gameObject.GetComponent<Abilities>();
        movement = gameObject.GetComponent<Movement>();

        Debug.Log("START" + photonView);
    }

/*    public void InitializeInputManager(Entity myEntity)
    {
        MyEntity = myEntity;
        photonView = MyEntity.gameObject.GetComponent<PhotonView>();
        abilities = MyEntity.gameObject.GetComponent<Abilities>();
        movement = MyEntity.gameObject.GetComponent<Movement>();
    }*/

    // Due to the nature of photon we must attach this script to the object with the photonview

    [PunRPC]
    public void Fire(Vector3 pos, int abilityNum)
    {
        abilities.castAbility(abilityNum, pos);
    }

    [PunRPC]
    public void Plant(Vector3 pos, int abilityNum)
    {
        abilities.plantAbility(abilityNum, pos);
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
                if (Input.GetButton("Fire1"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, 0);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, 0);
                    }
                }

                if (Input.GetButton("Fire2"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, 1);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, 1);
                    }
                }
                if (Input.GetButton("Fire3"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, 2);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, 2);
                    }
                }
                if (Input.GetButton("Fire4"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Fire(firePoint, 3);
                    }
                    else
                    {
                        this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, 3);
                    }
                }

                if (MyEntity.CanMove)
                {
                    movement.horizAxis = Input.GetAxis("HorizKey");
                    movement.vertAxis = Input.GetAxis("VertKey");
                }
            }

            if(MyEntity.IsPlanting)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Plant(firePoint, 0);
                    }
                    else
                    {
                        this.photonView.RPC("Plant", RpcTarget.AllBuffered, firePoint, 0);
                    }
                }

                if (Input.GetButton("Fire2"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Plant(firePoint, 1);
                    }
                    else
                    {
                        this.photonView.RPC("Plant", RpcTarget.AllBuffered, firePoint, 1);
                    }
                }
                if (Input.GetButton("Fire3"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Plant(firePoint, 2);
                    }
                    else
                    {
                        this.photonView.RPC("Plant", RpcTarget.AllBuffered, firePoint, 2);
                    }
                }
                if (Input.GetButton("Fire4"))
                {
                    if (!PhotonNetwork.IsConnected)
                    {
                        Plant(firePoint, 3);
                    }
                    else
                    {
                        this.photonView.RPC("Plant", RpcTarget.AllBuffered, firePoint, 3);
                    }
                }
            }
        }
    }

    public Vector3 GetPointerPosition()
    {
        return firePoint;
    }

    /*
    private void FireSecondary(Vector3 pos)
    {
        abilities.castAbility(1, pos);
    }

    private void FireSpell(Vector3 pos)
    {
        abilities.castAbility(2, pos);
    }

    private void FireUltimate(Vector3 pos)
    {
        abilities.castAbility(3, pos);
    }
    */
}
