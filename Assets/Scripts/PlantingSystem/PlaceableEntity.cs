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

    void Start(){
        Placed = false;
        Placing = false;
        Colliding = false;
    }

    void Update()
    {
        if (Placing && !Colliding)
        {
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
        Debug.Log("Collided with: " + other.gameObject.name);
        Colliding = true;
    }

    void OnTriggerExit(Collider other)
    {
        Colliding = false;
    }

    IEnumerator InvalidPlacementArea()
    {
        transform.Find("Canvas").gameObject.SetActive(true);
        Color c = gameObject.transform.Find("Canvas/InvalidPanel").GetComponent<Image>().color;
        yield return new WaitForSeconds(0.5f);
        for(float i=0.5f; i>=0; i-= 0.1f){
            c.a = i;
            gameObject.transform.Find("Canvas/InvalidPanel").GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        transform.Find("Canvas").gameObject.SetActive(false);
        c.a = 0.5f;
        gameObject.transform.Find("Canvas/InvalidPanel").GetComponent<Image>().color = c;
    }
}
