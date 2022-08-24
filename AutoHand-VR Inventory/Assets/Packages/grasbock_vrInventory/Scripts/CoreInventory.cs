using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI
{
    public class BaseInventory
    {
        public HashSet<CoreSlot> slots = new HashSet<CoreSlot>();

        //looks through all inventorySlots the Inventory has for one containing a specified ItemInfo
        //and returns that Inventory Slot (null if none was found)
        public virtual CoreSlot FindSlotWithItem(ItemInfo itemInfo)
        {
            foreach (CoreSlot slot in slots)
            {
                if (slot.ItemInfo.Equals(itemInfo))
                {
                    return slot;
                }
            }
            return null;
        }

    }
    
}