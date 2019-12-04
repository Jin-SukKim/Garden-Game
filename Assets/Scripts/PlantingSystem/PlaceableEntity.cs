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

    void OnTriggerEnter(Collider other){
        if(other.gameObject.GetComponent<Entity>() != null || other.gameObject.tag == "Environment")
        {
            Debug.Log("Colliding with " + other.gameObject.name);
            ParticleSystem ps = gameObject.transform.Find("CollisionIndicator").GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;
            main.startColor = new Color(0.8784314f, 0.2695822f, 0.2196078f, 0.4039216f);
            Colliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Entity>() != null || other.gameObject.tag == "Environment")
        {
            ParticleSystem ps = gameObject.transform.Find("CollisionIndicator").GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = ps.main;
            main.startColor = new Color(0.2193396f, 0.8773585f, 0.6492321f, 0.4039216f);
            Colliding = false;
        }
    }
}
