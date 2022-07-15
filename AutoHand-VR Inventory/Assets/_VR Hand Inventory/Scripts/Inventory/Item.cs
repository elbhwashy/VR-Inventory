using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public ItemProperties itemProperties;

}

[System.Serializable]
public class ItemProperties
{
    public string itemName;
    public GameObject itemPrefab;
}
