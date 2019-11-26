using UnityEngine;

using Photon;
using Photon.Pun;

public class InputManager : MonoBehaviour
{
    private Vector3 firePoint;

    private Entity MyEntity;

    //My entity components
    private PhotonView photonView;
    private Abilities abilities;
    private Movement movement;

    void Start()
    {
        /*
        photonView = gameObject.GetComponent<PhotonView>();
        abilities = gameObject.GetComponent<Abilities>();
        */
    }

    public void InitializeInputManager(Entity myEntity)
    {
        MyEntity = myEntity;
        photonView = MyEntity.gameObject.GetComponent<PhotonView>();
        abilities = MyEntity.gameObject.GetComponent<Abilities>();
        movement = MyEntity.gameObject.GetComponent<Movement>();
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

        if (photonView.IsMine || !PhotonNetwork.IsConnected)
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

                Vector3 direction = firePoint - MyEntity.gameObject.transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                MyEntity.gameObject.transform.rotation = Quaternion.Euler(0, rotation, 0);
            }


            if (Input.GetButton("Fire1"))
            {
                if (!PhotonNetwork.IsConnected)
                {
                    Debug.Log("NOT CONNECtED TO NEt");
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
                    Debug.Log("NOT CONNECtED TO NEt");
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
                    Debug.Log("NOT CONNECtED TO NEt");
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
                    Debug.Log("NOT CONNECtED TO NEt");
                    Fire(firePoint, 3);
                }
                else
                {
                    this.photonView.RPC("Fire", RpcTarget.AllBuffered, firePoint, 3);
                }
            }

            movement.horizAxis = Input.GetAxis("HorizKey");
            movement.vertAxis = Input.GetAxis("VertKey");
        }

        
    }
    [PunRPC]
    private void Fire(Vector3 pos, int abilityNum)
    {
        abilities.castAbility(abilityNum, pos);
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
