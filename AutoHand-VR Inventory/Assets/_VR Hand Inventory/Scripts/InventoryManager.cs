using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    [Header("toggle Controller References")]
    public InputActionReference PrimaryButtonController_Refrenece = null;


    public GameObject slotsParent;

    private void Awake()
    {
        slotsParent.SetActive(false);
    }

    private void OnEnable()
    {
        // set up input refrenece
        PrimaryButtonController_Refrenece.action.started += ToggleInventory; 
    }

    private void OnDisable()
    {
        PrimaryButtonController_Refrenece.action.started -= ToggleInventory; 
    }

    

    private void ToggleInventory(InputAction.CallbackContext obj)
    {
        if (slotsParent.activeInHierarchy)
        {
            slotsParent.SetActive(false);
        }
        else
        {
            slotsParent.SetActive(true);
        }
    }
}
