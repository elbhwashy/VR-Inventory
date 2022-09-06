using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Slot : MonoBehaviour
{
    [Header("Grib Controller References")]
    public InputActionReference RightGribController_Refrenece = null;
    public InputActionReference LeftGribController_Refrenece = null;


    [Space]
    public GameObject slotUI;
    public TextMesh itemsCountTxt;

    [Space]
    public string slotId;
    public string slotName;
    public int itemsCount;

    [Space]
    public List<Item> items = new List<Item>();

    private Item []resourcesItems;

    bool isControllerIn = false;
    GameObject preview;

    private void OnValidate()
    {
        //System.Guid newGUID = System.Guid.NewGuid();

        //slotId = newGUID + "";
    }

    private void Awake()
    {
        slotUI.SetActive(false);

        resourcesItems = Resources.LoadAll<Item>("Items/");

        InitializeSlot();

    }

    private void InitializeSlot()
    {
        Item slotItem = new Item();

        for (int i = 0; i < resourcesItems.Length; i++)
        {
            if (slotName == resourcesItems[i].itemProperties.itemName)
            {
                slotItem = resourcesItems[i];
            }
        }

        for (int i = 0; i < itemsCount; i++)
        {
            items.Add(slotItem);
            Item newItem = Instantiate(slotItem, transform);
            newItem.gameObject.SetActive(false);

            if (i == 0) preview = AddPreviewItemIntoSlot(newItem.gameObject , newItem.previewScaleFator);
            UpdateSlotUI();
        }
    }

    private void OnEnable()
    {
        // set up input refrenece
        RightGribController_Refrenece.action.started += OnClickGrib;
        LeftGribController_Refrenece.action.started += OnClickGrib;
    }

    private void OnDisable()
    {
        RightGribController_Refrenece.action.started -= OnClickGrib;
        LeftGribController_Refrenece.action.started -= OnClickGrib;
    }


    void Update()
    {
        //slowly rotate itemPreview around
        if (preview != null)
        {
            preview.transform.LookAt(
                preview.transform.position + Vector3.up,
                new Vector3(Mathf.Cos(Time.time), 0.0f, Mathf.Sin(Time.time))
            );
        }
    }

    private void OnClickGrib(InputAction.CallbackContext obj)
    {
        if (isControllerIn)
        { 
            // remove item from slot
            if(items.Count > 0)
            {

                Debug.Log("gwrefg");
                items[items.Count - 1].transform.position = transform.position;
                items[items.Count - 1].transform.rotation = transform.rotation;
                items[items.Count - 1].gameObject.SetActive(true);

                items.Remove(items[items.Count - 1]);

                UpdateSlotUI();

                if (items.Count == 0) RemovePreviewItemIntoSlot();
            }

           
        }
    } 



    private void OnTriggerEnter(Collider other)
    {
        if (!isControllerIn)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                if (items.Count > 0)
                {
                    if (item.itemProperties.itemName != items[0].itemProperties.itemName)
                        return;
                }

                AddItemToSlot(item);
            }
        }
        

        if (other.tag == "Hand")
        {
            isControllerIn = true;
        }
    }

    public void AddItemToSlot(Item item)
    {
        if (items.Count == 0) preview = AddPreviewItemIntoSlot(item.gameObject , item.previewScaleFator);
        items.Add(item);

        slotName = item.itemProperties.itemName;
        itemsCount = items.Count;

        // add item to slot
        item.gameObject.SetActive(false);
        item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;
        item.GetComponent<Rigidbody>().isKinematic = true;

        //update UI
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        itemsCountTxt.text = items.Count + "";
        if (items.Count > 1) slotUI.SetActive(true);
        else slotUI.SetActive(false);
    }

    private GameObject AddPreviewItemIntoSlot(GameObject item , float previewScaleFactor)
    { 
        GameObject preview = Instantiate(item, transform.position, transform.rotation);

        preview.transform.parent = transform;
        preview.transform.localScale = new Vector3(preview.transform.localScale.x * previewScaleFactor, 
            preview.transform.localScale.y * previewScaleFactor, preview.transform.localScale.z * previewScaleFactor);

        //remove all components that are not used for visuals
        var components = preview.GetComponents<Component>();
        foreach (Component comp in components)
        {
            if (!(comp is MeshRenderer) &&
                !(comp is MeshFilter) &&
                !(comp is Transform)
            )
            {
                Destroy(comp);
            }
        }

        preview.SetActive(true);

        return preview;
    }

    private void RemovePreviewItemIntoSlot()
    {
        Destroy(transform.GetChild(1).gameObject); 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            isControllerIn = false;
        }
    }

}
