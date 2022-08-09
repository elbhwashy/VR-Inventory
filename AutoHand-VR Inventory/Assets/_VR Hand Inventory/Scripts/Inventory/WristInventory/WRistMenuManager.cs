using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WRistMenuManager : MonoBehaviour
{
    [Header("Controller Reference")]
    public InputActionReference SecondaryButtonRefrenece = null;

    public GameObject wRistMenu;
    public GameObject inventoryPanel, itemsPanel;


    private void OnEnable()
    {
        // set up input refrenece
        SecondaryButtonRefrenece.action.started += ToggleWRistMenu; 
    }

    private void OnDisable()
    {
        SecondaryButtonRefrenece.action.started -= ToggleWRistMenu; 
    }

    private void ToggleWRistMenu(InputAction.CallbackContext obj)
    {
        if (wRistMenu.activeInHierarchy)
        {
            wRistMenu.SetActive(false);
        }
        else
        {
            wRistMenu.SetActive(true);
        }
    }

    private void Awake()
    {
        wRistMenu.SetActive(false);
    }

    public void OpenItemsPanel()
    {
        inventoryPanel.SetActive(false);
        itemsPanel.SetActive(true);
    }

    public void BackToInventory()
    {
        inventoryPanel.SetActive(true);
        itemsPanel.SetActive(false);
    }

    public void CloseWRistMenu()
    {
        wRistMenu.SetActive(false);
    }

  

}
