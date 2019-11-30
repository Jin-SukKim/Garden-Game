using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlantingSystem : MonoBehaviour
{
    public GameObject CurGO;

    //The currently selected prefab
    private GameObject curPrefab;

    public GameObject GridObject;

    public Entity MyEntity;

    private enum PlantType { shootrose, mortartulip }

    private PlantType SelectedPlant;

    private Dictionary<string, GameObject> plants = new Dictionary<string, GameObject>();

    void Start()
    {
        MyEntity = gameObject.GetComponent<Entity>();

        plants = PrefabManager.ReturnPlacables();

        SelectedPlant = PlantType.shootrose;
        CurGO = PrefabManager.SpawnPlaceable("shootrose", MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Cancelling Placement");
            Destroy(this);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (CurGO.activeSelf)
            {
                CurGO.GetComponent<PlaceableEntity>().Placing = true;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            CurGO.GetComponent<PlaceableEntity>().Placing = false;
            if (CurGO.GetComponent<PlaceableEntity>().Placed)
            {
                Destroy(this);
            }
        }

        if (Input.GetButton("Fire3"))
        {
            ToggleDown();
        }

        if (Input.GetButton("Fire4"))
        {
            ToggleUp();
        }
    }

    public void ToggleUp()
    {
        SelectedPlant = (int)SelectedPlant + 1 >= Enum.GetValues(typeof(PlantType)).Length ? 0 : SelectedPlant++;
        Destroy(CurGO);
        CurGO = PrefabManager.SpawnPlaceable(SelectedPlant.ToString(), MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
    }

    public void ToggleDown()
    {
        SelectedPlant = (int)SelectedPlant - 1 < 0 ? (PlantType)Enum.GetValues(typeof(PlantType)).Length - 1 : 0;
        Destroy(CurGO);
        CurGO = PrefabManager.SpawnPlaceable(SelectedPlant.ToString(), MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
    }

    private void placeGO(Vector3 position)
    {
        CurGO = Instantiate(curPrefab, position, Quaternion.identity);
        CurGO.transform.parent = GridObject.transform;
    }

    //Occurs after Update
    void LateUpdate()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.gameObject.layer.ToString() == "Plane")
            {
                Debug.Log("move");
                if (CurGO == null)
                {
                    placeGO(hitInfo.point);
                }
                CurGO.SetActive(true);
                CurGO.transform.position = hitInfo.point;
                CurGO.GetComponent<PlaceableEntity>().moveThis();
            }
        }
    }
}
