﻿using System.Collections;
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

    public enum PlantType { shootrose, mortartulip }

    public PlantType SelectedPlant;

    private Dictionary<string, GameObject> plants = new Dictionary<string, GameObject>();

    void Start()
    {
        Debug.Log("Planting system started");
        MyEntity = gameObject.GetComponent<Entity>();

        plants = PrefabManager.ReturnPlacables();

        SelectedPlant = PlantType.shootrose;
        CurGO = PrefabManager.SpawnPlaceable("shootrose", MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
        CurGO.GetComponent<Entity>().IsDisabled = true;
        CurGO.transform.Find("CollisionIndicator").gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            MyEntity.IsPlanting = false;
            Destroy(CurGO);
            Destroy(this);
        }

        if (Input.GetButton("Fire1"))
        {
            CurGO.GetComponent<PlaceableEntity>().Placing = true;

            if (CurGO.GetComponent<PlaceableEntity>().Placed)
            {
                MyEntity.IsPlanting = false;
                gameObject.GetComponent<Abilities>().plantAbility((int)SelectedPlant, CurGO.transform.position);
                Destroy(CurGO);
                Destroy(this);
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if(CurGO.GetComponent<PlaceableEntity>() != null)
                CurGO.GetComponent<PlaceableEntity>().Placing = false;
        }

        if (Input.GetButtonDown("Fire3"))
        {
            ToggleDown();
        }

        if (Input.GetButtonDown("Fire4"))
        {
            ToggleUp();
        }
    }

    public void ToggleUp()
    {
        SelectedPlant = (int)SelectedPlant + 1 >= Enum.GetValues(typeof(PlantType)).Length ? 0 : SelectedPlant++;
        Destroy(CurGO);
        CurGO = PrefabManager.SpawnPlaceable(SelectedPlant.ToString(), MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
        CurGO.transform.Find("CollisionIndicator").gameObject.SetActive(true);
    }

    public void ToggleDown()
    {
        SelectedPlant = (int)SelectedPlant - 1 < 0 ? (PlantType)Enum.GetValues(typeof(PlantType)).Length - 1 : 0;
        Destroy(CurGO);
        CurGO = PrefabManager.SpawnPlaceable(SelectedPlant.ToString(), MyEntity.gameObject.GetComponent<InputManager>().GetPointerPosition(), MyEntity);
        CurGO.transform.Find("CollisionIndicator").gameObject.SetActive(true);
    }

    //Occurs after Update
    void LateUpdate()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.gameObject.layer.ToString() == "8")
            {
                CurGO.transform.position = hitInfo.point;
                CurGO.GetComponent<PlaceableEntity>().moveThis();
            }
        }
    }
}
