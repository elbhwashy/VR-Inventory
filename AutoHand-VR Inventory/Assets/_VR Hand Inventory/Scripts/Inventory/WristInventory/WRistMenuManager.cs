using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WRistMenuManager : MonoBehaviour
{
    [Header("Controller Reference")]
    public InputActionReference SecondaryButtonRefrenece = null;

    public WRistListManager WRistListManager;

    [Header("Panels")]
    public GameObject wRistMenu;
    public GameObject introPanel, itemsPanel, infoPanel, confortPanel;


    private void Awake()
    {
        wRistMenu.SetActive(false);
        itemsPanel.SetActive(false);
        introPanel.SetActive(true);
    }
    private void OnEnable()
    {
        // set up input refrenece
        SecondaryButtonRefrenece.action.started += ToggleWRistMenu;
    }

    private void OnDisable()
    {
        SecondaryButtonRefrenece.action.started -= ToggleWRistMenu; 
    }

    private void Start()
    {
        WRistListManager.setUpMenu();
    }

    public void ToggleWRistMenu(InputAction.CallbackContext obj)
    {
        if (wRistMenu.activeInHierarchy)
        {
            wRistMenu.SetActive(false);
            //WRistListManager.SaveData();
        }
        else
        {
            wRistMenu.SetActive(true); 
        }

    }
    
    public void ToggleWRistMenu()
    {
        if (wRistMenu.activeInHierarchy)
        {
            wRistMenu.SetActive(false);
            WRistListManager.SaveData();
        }
        else
        {
            wRistMenu.SetActive(true); 
        }

    }
    public void OpenPanel(GameObject panel)
    {
        introPanel.SetActive(false);
        panel.SetActive(true);
    }
    
    public void ClosePanel(GameObject panel)
    {
        introPanel.SetActive(true);
        panel.SetActive(false);
    } 

    public void CloseWRistMenu()
    {
        wRistMenu.SetActive(false);
    }

  

}
