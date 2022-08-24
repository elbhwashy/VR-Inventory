using GVRI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

[Serializable]
public class InventorySaveAndLoad : MonoBehaviour
{
    public static Action OnInitialised;

    [SerializeField]
    private PathController pathController;

    [SerializeField]
    private List<GVRI.Slot> slots;

    private void OnEnable()
    {
        PathController.OnPathCreated += LoadInventory;
    }

    private void OnDisable()
    {
        PathController.OnPathCreated -= LoadInventory;

        Save();
    }

    [ContextMenu("FetchSlots")]
    private void FetchSlots()
    {
        slots = new List<GVRI.Slot>();
        slots = FindObjectsOfType<GVRI.Slot>().ToList();
    }


    private void LoadInventory()
    {
        Load();

        OnInitialised?.Invoke();
    }

    [ContextMenu("Save")]
    private void Save()
    {
        if (slots == null || slots.Count == 0) return;

        BinaryFormatter bf = new BinaryFormatter();

        string path = Path.Combine(pathController.RootPath, "Inventory.dat");
        FileStream file = File.Create(path);

        InventorySave save = new InventorySave();
        save.savedSlots = new List<MySlot>();

        foreach (GVRI.Slot aSlot in slots)
        {
            MySlot mySlot = new MySlot();
            mySlot.slotID = aSlot.SlotID;
            if (aSlot.CoreSlot.ItemInfo)
            {
                mySlot.itemID = aSlot.CoreSlot.ItemInfo.ID;
                mySlot.itemCount = aSlot.CoreSlot.ItemCount;
            }

            save.savedSlots.Add(mySlot);
        }

        bf.Serialize(file, save);
        file.Close();
    }

    private void Load()
    {
        string path = Path.Combine(pathController.RootPath, "Inventory.dat");

        if (File.Exists(path) && new FileInfo(path).Length != 0)
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);

            InventorySave save = (InventorySave)bf.Deserialize(file);
            file.Close();

            foreach (MySlot mySlot in save.savedSlots)
            {
                GVRI.Slot slot = slots.Find(x => x.SlotID == mySlot.slotID);

                if (slot)
                {
                    if (mySlot.itemID != -1)
                    {
                        slot.SetItem(mySlot.itemID, mySlot.itemCount);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    [Serializable]
    private class InventorySave
    {
        public List<MySlot> savedSlots;
    }

    [Serializable]
    private class MySlot
    {
        public int slotID;
        public int itemID = -1;
        public uint itemCount;
    }
}
