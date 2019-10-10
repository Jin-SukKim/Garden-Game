using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableEntity : MonoBehaviour
{
    [SerializeField]
    private bool Placed;
    void Start(){
        Placed = false;
    }

    //Snaps to grid
    public void moveThis(){
        Debug.Log("moved");
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

    public void PlaceThis()
    {
        if(FindObjectOfType<PlacingScript>().CurGO != this.gameObject)
            StartCoroutine("InvalidPlacementArea");
        else{
            Placed = true;
            gameObject.layer = 2;
        }
    }

    void OnTriggerEnter(Collider other){
        Debug.Log("Collided");
        if(!Placed)
            gameObject.SetActive(false);
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
