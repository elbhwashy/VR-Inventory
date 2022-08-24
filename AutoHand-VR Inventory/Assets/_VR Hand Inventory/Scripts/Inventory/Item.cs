using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
   public ItemProperties itemProperties;
   public ItemStatus itemStatus;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnGrabItem()
    {
        rb.isKinematic = false;
        itemStatus = ItemStatus.Grabbed;
    }

    public void OnRelaseItem()
    {
        rb.isKinematic = true;
        itemStatus = ItemStatus.Idle;
    }

}

[System.Serializable]
public class ItemProperties
{
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPrefab;
}

[System.Serializable]
public enum ItemStatus
{
    Idle,
    Grabbed
}