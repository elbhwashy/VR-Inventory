﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVRI { 
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "Inventory/ItemInfo")]
    public class ItemInfo : ScriptableObject
    {
        public new string name;
        public GameObject prefab;
    }
}