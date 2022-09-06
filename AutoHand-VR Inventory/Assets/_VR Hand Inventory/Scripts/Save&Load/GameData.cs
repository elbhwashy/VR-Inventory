using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
	#region WristMenu Data

	[Serializable]
	public class WristMenuDataModel
	{
		public List<SlotsData> slotsData = new List<SlotsData>();	
	}

	[Serializable]
	public class SlotsData
	{ 
		public string slotName; 
		public string itemsCount; 
		public List<ItemData> itemsData = new List<ItemData>(); 
	}

	[Serializable]
	public class ItemData
	{
        public string itemId;
        public string itemName;
	}

	#endregion

	#region WRaistInventory Data

	[Serializable]
	public class WRaistDataModel
	{ 
		public List<SlotsData> slotsData = new List<SlotsData>();
	}

	#endregion
}
