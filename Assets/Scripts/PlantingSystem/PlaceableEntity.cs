using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon;
using Photon.Pun;
using Photon.Realtime;

public class PlaceableEntity : MonoBehaviour
{
    public bool Placed;
    
    public bool Placing;

    private bool Colliding;

    private Entity MyEntity;

    public void Init(Entity e)
    {
        MyEntity = e;
    }


    void Update()
    {
        if (Placing && !Colliding)
        {
            moveThis();
            Placed = true;
        }
    }

    //Snaps to grid
    public void moveThis(){
        Grid grid = GameObject.Find("GridHolder").GetComponent<Grid>();
        Vector3Int cellPos = grid.LocalToCell(transform.localPosition);
        transform.localPosition = grid.GetCellCenterLocal(cellPos);
    }

    public int returnX(){
        return transform.parent.GetComponent<Grid>().LocalToCell(transform.localPosition).x;
    }

    public int returnZ(){
        return transform.parent.GetComponent<Grid>().LocalToCell(transform.localPosition).z;
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.GetComponent<Entity>() != null)
        {
            Debug.Log("Colliding with " + other.gameObject.name);
            gameObject.transform.Find("CollisionIndicator").GetComponent<ParticleSystem>().startColor = new Color(255, 0, 0, 120);
            Colliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>() != null)
        {
            gameObject.transform.Find("CollisionIndicator").GetComponent<ParticleSystem>().startColor = new Color(0, 255, 0, 120);
            Colliding = false;
        }
    }
}
