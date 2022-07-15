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
    public TextMesh itemsCount;
    
    [Space]
    public List<Item> items = new List<Item>();

    bool isControllerIn = false;
    GameObject preview;
    private void Awake()
    {
        slotUI.SetActive(false);
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
    {//slowly rotate itemPreview around
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
        Debug.Log(other.name + " sddf ");
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

                if (items.Count == 0) preview = AddPreviewItemIntoSlot(other.gameObject);
                items.Add(item);

                // add item to slot
                other.gameObject.SetActive(false); 
                other.transform.position = transform.position; 
                other.transform.rotation = transform.rotation; 

                //update UI
                UpdateSlotUI();
            }
        }
        

        if (other.tag == "Hand")
        {
            isControllerIn = true;
        }
    }

    private void UpdateSlotUI()
    {
        itemsCount.text = items.Count + "";
        if (items.Count > 1) slotUI.SetActive(true);
        else slotUI.SetActive(false);
    }

    private GameObject AddPreviewItemIntoSlot(GameObject item)
    { 
        GameObject preview = Instantiate(item, transform.position, transform.rotation);

        preview.transform.parent = transform;
        preview.transform.localScale = new Vector3(preview.transform.localScale.x / 2, 
            preview.transform.localScale.y/2, preview.transform.localScale.z/2);

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
