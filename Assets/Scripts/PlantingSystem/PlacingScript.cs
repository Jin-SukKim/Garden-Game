using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingScript : MonoBehaviour
{
    //Whether user is currently placing
    public bool Placing;

    //The game object to be placed
    private GameObject curGO;
    public GameObject CurGO { get{ return curGO; } }

    //The currently selected prefab
    private GameObject curPrefab;

    //The current rotation of the object to be placed
    private float curRotation;

    //UI canvas
    //public GameObject TestingCanvas;

    public GameObject GridObject;

    //private int[,] mapArray= new int[10,10];

    //Prefabs
    [SerializeField]
    GameObject PlaceableArea;
    [SerializeField]
    GameObject Plant1;
    [SerializeField]
    GameObject Plant2;

    //GridSpace[] mapArray= new GridSpace;

    //Called on the first frame
    void Start()
    {
        //TestingCanvas = GameObject.Find("TestingCanvas");
        curRotation = 0f;
        Placing = false;
        GridObject = GameObject.Find("planes/GridHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            Debug.Log("Starting placement of slot 1");
            Destroy(curGO);
            curPrefab = Plant1;
            Placing = true;
        }

        if(Input.GetKeyDown("2"))
        {
            Debug.Log("Starting placement of slot 2");
            Destroy(curGO);
            curPrefab = Plant2;
            Placing = true;
        }

        if(Input.GetKeyDown("0"))
        {
            Debug.Log("Starting placement of slot 0");
            Destroy(curGO);
            curPrefab = PlaceableArea;
            Placing = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hitInfo))
            {
                Grid grid = GridObject.GetComponent<Grid>();
                Vector3Int cellPos = grid.LocalToCell(hitInfo.point);
                Debug.Log(hitInfo.transform.gameObject.name);
                Debug.Log(cellPos + " " + grid.GetCellCenterLocal(cellPos));
            }
        }


        if(Placing)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Debug.Log("Cancelling Placement");
                Placing = false;
            }

            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if(Physics.Raycast(ray, out hitInfo))
                {
                    Debug.Log(hitInfo.transform.gameObject.name);
                    //Needs to be offset by 5 to allow for negative positions
                    if(hitInfo.transform.gameObject != curGO){
                        Debug.Log("Spot already taken");
                        hitInfo.transform.gameObject.GetComponent<PlaceableEntity>().PlaceThis();
                    }else{
                        Debug.Log("Placed object");
                        if(curGO.activeSelf){
                            curGO.GetComponent<PlaceableEntity>().PlaceThis();
                            curGO = null;
                            Placing = false;
                        }
                    }
                    
                }
            }
            /*
            if(Input.GetKeyDown("q"))
            {
                curRotation -= 45;
                curGO.transform.Rotate(0f, -45f, 0f, Space.Self);
            }

            if(Input.GetKeyDown("e"))
            {
                curRotation += 45;
                curGO.transform.Rotate(0f, 45f, 0f, Space.Self);
            }
            */
        }
    }

    private void placeGO(Vector3 position){
        curGO = Instantiate(curPrefab, position, Quaternion.identity);
        curGO.transform.parent = GridObject.transform;
    }

    //Occurs after Update
    void LateUpdate()
    {
        //Moves the selected gameobject
        if(Placing)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int plantMask = 1 << 9;

            if(Physics.Raycast(ray, out hitInfo))
            {
                if(hitInfo.transform.gameObject.layer==8)
                {
                    Debug.Log("move");
                    if(curGO == null){
                        placeGO(hitInfo.point);
                    }
                    curGO.SetActive(true);
                    curGO.transform.position = hitInfo.point;
                    curGO.GetComponent<PlaceableEntity>().moveThis();
                }
            }
        }else {
            Destroy(curGO);
        }
    }
}
