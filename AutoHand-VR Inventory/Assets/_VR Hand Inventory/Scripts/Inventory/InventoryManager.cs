using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static GameData;

public class InventoryManager : MonoBehaviour
{
    [Header("toggle Controller References")]
    public InputActionReference PrimaryButtonController_Refrenece = null;

    public List<Slot> wRaistSlots = new List<Slot>(); 

    public GameObject slotsParent;
    public Transform playerPos;

    [SerializeField]
    private Item[] sceneItems;

    WRaistDataModel wRaistDataModel;


    private void Awake()
    {
        sceneItems = FindObjectsOfType<Item>();

        slotsParent.SetActive(false);

        LoadWRaistData();
    } 

    private void OnEnable()
    {
        // set up input refrenece
        PrimaryButtonController_Refrenece.action.started += ToggleInventory; 
    }

    private void OnDisable()
    {
        PrimaryButtonController_Refrenece.action.started -= ToggleInventory; 
    }

    private void Update()
    {
        transform.localPosition = playerPos.localPosition;
        //transform.localRotation = playerPos.localRotation;
    }

    private void ToggleInventory(InputAction.CallbackContext obj)
    {
        if (slotsParent.activeInHierarchy)
        {
            slotsParent.SetActive(false);
            SaveWRaistData();
        }
        else
        {
            slotsParent.SetActive(true);
        }
    }


    public void SaveWRaistData()
    {
        wRaistDataModel = new WRaistDataModel();

        for (int i = 0; i < wRaistSlots.Count; i++)
        {
            SlotsData slotsData = new SlotsData();

            for (int y = 0; y < wRaistSlots[i].itemsCount; y++)
            {
                ItemData itemData = new ItemData();

                itemData.itemId = wRaistSlots[i].items[y].itemProperties.itemId + "";
                itemData.itemName = wRaistSlots[i].items[y].itemProperties.itemName;

                Debug.Log("sdvdfsd" + itemData.itemName);
                slotsData.itemsData.Add(itemData);
            }

            slotsData.slotName = wRaistSlots[i].slotName;
            slotsData.itemsCount = wRaistSlots[i].itemsCount + "";

            wRaistDataModel.slotsData.Add(slotsData);
        }

        string json = JsonUtility.ToJson(wRaistDataModel, true);
        File.WriteAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name +  "_WRaistMenuData.json", json);
        Debug.Log("Saved:  " + Application.persistentDataPath);
    }

    public void LoadWRaistData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_WRaistMenuData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_WRaistMenuData.json");
            wRaistDataModel = JsonUtility.FromJson<WRaistDataModel>(json);

            if (wRaistDataModel != null)
            {
                for (int i = 0; i < wRaistDataModel.slotsData.Count; i++)
                {
                    wRaistSlots[i].slotName = wRaistDataModel.slotsData[i].slotName;
                    wRaistSlots[i].itemsCount = int.Parse(wRaistDataModel.slotsData[i].itemsCount);


                    for (int y = 0; y < wRaistDataModel.slotsData[i].itemsData.Count; y++)
                    {
                        for (int z = 0; z < sceneItems.Length; z++)
                        {
                            //Debug.Log("wdfefwefwe:  " + int.Parse(wristMenuDataModel.slotsData[i].itemsData[y].itemName)  +" vdvfdv " + resourcesItems[z].itemProperties.itemName);
                            if (wRaistDataModel.slotsData[i].itemsData[y].itemId == sceneItems[z].itemProperties.itemId)
                            {
                                sceneItems[z].gameObject.SetActive(false);
                                wRaistSlots[i].AddItemToSlot(sceneItems[z]);
                            }
                        }
                    }
                }
            }
        }
    }

}
