using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUICell : MonoBehaviour
{
    public WRistListManager WRistListManager;

    public Image ItemImage;
    public TMPro.TextMeshProUGUI ItemName;
    public TMPro.TextMeshProUGUI ItemCount;


    public void SetItemProp(Sprite itemSprite, string itemName, string itemCount)
    {
        ItemImage.sprite = itemSprite;
        ItemName.text = itemName; 
    }


    public void OnClickItem()
    {
        WRistListManager.OnDragItemFromMenu(this);
    }
}
