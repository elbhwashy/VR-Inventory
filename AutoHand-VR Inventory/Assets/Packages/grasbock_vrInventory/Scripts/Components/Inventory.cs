using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI
{
    public class Inventory : MonoBehaviour
    {
        public BaseInventory baseInv;
        public List<Slot> slots;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (baseInv == null) baseInv = new BaseInventory();
            if (slots == null) slots = new List<Slot>();

            //get all child objects with Slot component
            UpdateSlots();
            Close(); //to prevent the inventory staying open at startup
        }

        public virtual Slot FindSlotWithItem(ItemInfo itemInfo)
        {
            foreach (Slot s in slots)
            {
                if (s.CoreSlot.ItemInfo.Equals(itemInfo))
                {
                    return s;
                }
            }
            return null;
        }

        public virtual Slot FindSlotWithSlot(CoreSlot slot)
        {
            foreach (Slot s in slots)
            {
                if (s.CoreSlot.Equals(slot))
                {
                    return s;
                }
            }
            return null;
        }

        public virtual void UpdateSlots()
        {
            //get all child objects with inventorySlot component
            slots = new List<Slot>(GetComponentsInChildren<Slot>());
            foreach(Slot s in slots)
            {
                baseInv.slots.Add(s.CoreSlot);
            }
        }

        public virtual void Open()
        {
            foreach (Slot s in slots)
            {
                s.Open();
            }
        }

        public virtual void Close()
        {
            foreach (Slot s in slots)
            {
                s.Close();
            }
        }
    }
}