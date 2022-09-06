using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameData;

public class WRistListManager : MonoBehaviour
{
    public GameObject hint;
    public ItemUICell itemUIPrefab;
    public List<WRistSlot> initialWRistSlots = new List<WRistSlot>();
    public List<WRistSlot> wRistSlots = new List<WRistSlot>();

    public Transform rightHand;
    public Transform itemsParent;
    public Transform UIItemsParent;

    WristMenuDataModel wristMenuDataModel;

     
    [SerializeField]
    private Item[] sceneItems; 

    private void OnDisable()
    {
        SaveData();
    }

    internal void setUpMenu()
    {
        // load the items from resources folder
        //resourcesItems = Resources.LoadAll<Item>("Items/");
        sceneItems = FindObjectsOfType<Item>();

        // Load WRist Menu data
        LoadWRistMenuData();

        itemUIPrefab.gameObject.SetActive(false);

        InitializeMenu();
    }

    private void InitializeMenu()
    {
        if (wRistSlots.Count != 0)
        {
            hint.SetActive(false);

            for (int i = 0; i < wRistSlots.Count; i++)
            {
                ItemUICell newUIItem = Instantiate(itemUIPrefab, UIItemsParent);

                Item slotItem = new Item();

                for (int y = 0; y < wRistSlots[i].itemsCount; y++)
                {
                    for (int z = 0; z < sceneItems.Length; z++)
                    {
                        if(wristMenuDataModel.slotsData[i].itemsData[y].itemId == sceneItems[z].itemProperties.itemId)
                        {
                            slotItem = sceneItems[z];

                            slotItem.transform.parent = newUIItem.transform;
                            slotItem.transform.localPosition = Vector3.zero;
                            slotItem.transform.localRotation = Quaternion.identity;
                            slotItem.gameObject.SetActive(false);

                            wRistSlots[i].items.Add(slotItem);
                        }
                    }
                }

                newUIItem.SetItemProp(slotItem.itemProperties.itemImage,
                    slotItem.itemProperties.itemName, (wRistSlots[i].itemsCount + ""));


                newUIItem.gameObject.SetActive(true);

                wRistSlots[i].UIItem = newUIItem;                
            }
        }
    }    

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        
        if(item != null  && item.itemStatus == ItemStatus.Grabbed)
        {
            // check if this item added before 
            for (int i = 0; i < wRistSlots.Count; i++)
            {
                if(wRistSlots[i].SlotName == item.itemProperties.itemName)
                {
                    wRistSlots[i].items.Add(item);
                    wRistSlots[i].UIItem.ItemCount.text = wRistSlots[i].items.Count  + "";
                    wRistSlots[i].itemsCount++;

                    item.gameObject.SetActive(false);
                    return;
                }
            }

            // this item not added before & create a new slot 
            ItemUICell newItem = Instantiate(itemUIPrefab, UIItemsParent);
            newItem.SetItemProp(item.itemProperties.itemImage, item.itemProperties.itemName, "1");
            newItem.gameObject.SetActive(true);

            WRistSlot newWRistSlot = new WRistSlot();
            newWRistSlot.AddItem(item.itemProperties.itemName, item, newItem);

            newWRistSlot.UIItem.ItemCount.text = newWRistSlot.items.Count + "";
            newWRistSlot.itemsCount++;

            wRistSlots.Add(newWRistSlot);

            item.transform.parent = newItem.transform;
            item.gameObject.SetActive(false);


            if (hint.activeInHierarchy) hint.SetActive(false);
        }
    }


    public void OnDragItemFromMenu(ItemUICell uIItem)
    {
        for (int i = 0; i < wRistSlots.Count; i++)
        {
            if(wRistSlots[i].SlotName == uIItem.ItemName.text)
            {
                wRistSlots[i].items[0].transform.position = rightHand.position;
                wRistSlots[i].items[0].transform.parent = itemsParent;
                wRistSlots[i].items[0].gameObject.SetActive(true);

                wRistSlots[i].items.Remove(wRistSlots[i].items[0]);

                wRistSlots[i].UIItem.ItemCount.text = wRistSlots[i].items.Count +"";

                if(wRistSlots[i].items.Count ==0)
                {
                    wRistSlots.Remove(wRistSlots[i]);
                    Destroy(uIItem.gameObject);
                }
            }
        }
    }



    public void SaveData()
    {
        // here we will save the game data .
        SaveWRistMenuData(wRistSlots);
    }

    public  void SaveWRistMenuData(List<WRistSlot> _WRistSlots)
    {
        wristMenuDataModel = new WristMenuDataModel();

        for (int i = 0; i < _WRistSlots.Count; i++)
        {
            SlotsData slotsData = new SlotsData();

            for (int y = 0; y < _WRistSlots[i].itemsCount; y++)
            {
                ItemData itemData = new ItemData();

                itemData.itemId = _WRistSlots[i].items[y].itemProperties.itemId + "";
                itemData.itemName = _WRistSlots[i].items[y].itemProperties.itemName; 

                Debug.Log("sdvdfsd" + itemData.itemName);
                slotsData.itemsData.Add(itemData);
            }

            slotsData.slotName = _WRistSlots[i].SlotName;
            slotsData.itemsCount = _WRistSlots[i].itemsCount + "";

            wristMenuDataModel.slotsData.Add(slotsData);
        }

        string json = JsonUtility.ToJson(wristMenuDataModel, true);
        File.WriteAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name  + "_WRistMenuData.json", json);
        Debug.Log("Saved:  " + Application.persistentDataPath);
    }

    public void LoadWRistMenuData()
    {
        if(File.Exists(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_WRistMenuData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + SceneManager.GetActiveScene().name + "_WRistMenuData.json");
            wristMenuDataModel = JsonUtility.FromJson<WristMenuDataModel>(json);

            if (wristMenuDataModel != null)
            {
                for (int i = 0; i < wristMenuDataModel.slotsData.Count; i++)
                {
                    WRistSlot newWRistSlot = new WRistSlot();

                    newWRistSlot.SlotName = wristMenuDataModel.slotsData[i].slotName;
                    newWRistSlot.itemsCount = int.Parse(wristMenuDataModel.slotsData[i].itemsCount);

                    wRistSlots.Add(newWRistSlot);

                    for (int y = 0; y < wristMenuDataModel.slotsData[i].itemsData.Count; y++)
                    {
                        for (int z = 0; z < sceneItems.Length; z++)
                        {
                            //Debug.Log("wdfefwefwe:  " + int.Parse(wristMenuDataModel.slotsData[i].itemsData[y].itemName)  +" vdvfdv " + resourcesItems[z].itemProperties.itemName);
                            if (wristMenuDataModel.slotsData[i].itemsData[y].itemId == sceneItems[z].itemProperties.itemId)
                            {
                                sceneItems[z].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            // create the initial file 
            SaveWRistMenuData(initialWRistSlots);
            LoadWRistMenuData();
        }
    }
}


[System.Serializable]
public class WRistSlot
{
    public string SlotName;
    public ItemUICell UIItem;
    public int itemsCount;
    public List<Item> items = new List<Item>();


    public void AddItem(string name, Item item, ItemUICell itemUICell)
    {
        SlotName = name;
        items.Add(item);
        UIItem = itemUICell;
    }
}
