using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WRistListManager : MonoBehaviour
{
    public GameObject hint;
    public ItemUICell itemUIPrefab;
    public List<WRistSlot> wRistSlots = new List<WRistSlot>();

    private void Start()
    {
        itemUIPrefab.gameObject.SetActive(false);
        if(wRistSlots.Count == 0)
        {
            hint.SetActive(false);
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


                    item.gameObject.SetActive(false);
                    return;
                }
            }

            // this item not added before & create a new slot 
            ItemUICell newItem = Instantiate(itemUIPrefab, transform);
            newItem.SetItemProp(item.itemProperties.itemImage, item.itemProperties.itemName, "1");
            newItem.gameObject.SetActive(true);

            WRistSlot newWRistSlot = new WRistSlot();
            newWRistSlot.AddItem(item.itemProperties.itemName, item, newItem);

            wRistSlots.Add(newWRistSlot);

            item.gameObject.SetActive(false);

            if (hint.activeInHierarchy) hint.SetActive(false);

        }
    }
}


[System.Serializable]
public class WRistSlot
{
    public string SlotName;
    public ItemUICell UIItem;
    public List<Item> items = new List<Item>();


    public void AddItem(string name, Item item, ItemUICell itemUICell)
    {
        SlotName = name;
        items.Add(item);
        UIItem = itemUICell;
    }
}
