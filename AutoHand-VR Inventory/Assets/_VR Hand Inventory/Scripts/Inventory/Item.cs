using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
   public ItemProperties itemProperties;
   public ItemStatus itemStatus;

    public float previewScaleFator = 0.5f;
    private Rigidbody rb;


    private void OnValidate()
    {
        //System.Guid newGUID = System.Guid.NewGuid();

        //itemProperties.itemId = newGUID + "";
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    public void OnGrabItem()
    {
        rb.isKinematic = false;
        itemStatus = ItemStatus.Grabbed;
    }

    public void OnGrabItem_Bow()
    {
        itemStatus = ItemStatus.Grabbed;
    }

    public void OnRelaseItem()
    {
        rb.isKinematic = true;
        itemStatus = ItemStatus.Idle;
    }
    
    public void OnRelaseItem_Bow()
    { 
        itemStatus = ItemStatus.Idle;
    }

}

[System.Serializable]
public class ItemProperties
{
    public string itemId ;
    public string itemName;
    public Sprite itemImage;
    //public GameObject itemPrefab;
}

[System.Serializable]
public enum ItemStatus
{
    Idle,
    Grabbed
}