using Autohand;
using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Controller Reference")]
    public InputActionReference PrimaryButtonRefrenece = null;

    [Header("Main Menu")]
    public GameObject mainMenu;
    
    [Header("Player")]
    public Transform playerPosition;
    public SmoothLocomotion smoothLocomotion;

    [Space]
    public GameObject LoadingSphere;
    private void Awake()
    {
        mainMenu.SetActive(false);
    }

    private void OnEnable()
    {
        // set up input refrenece
        PrimaryButtonRefrenece.action.started += ToggleMainMenu;

        //OVRManager.HMDMounted += HandleHMDMounted;
        OVRManager.HMDUnmounted += HandleHMDUnmounted;

    }


    private void OnDisable()
    {
        PrimaryButtonRefrenece.action.started -= ToggleMainMenu;

        //OVRManager.HMDMounted -= HandleHMDMounted;
        OVRManager.HMDUnmounted -= HandleHMDUnmounted;
    }

    void HandleHMDMounted()
    {
        PauseGame();
    }

    void HandleHMDUnmounted()
    {
        PauseGame();
    }

    private void ToggleMainMenu(InputAction.CallbackContext obj)
    {
        if (mainMenu.activeInHierarchy)
        {
            // Pause the game
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        mainMenu.transform.position = playerPosition.transform.position;
        mainMenu.SetActive(true);
        smoothLocomotion.enabled = false;

    }

    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        smoothLocomotion.enabled = true;

    }

    public void MovePlayerToStartPostion()
    {
        LoadingSphere.SetActive(true);
        Invoke("PlayerToStartPos", 3f);
    }

    void PlayerToStartPos()
    {
        smoothLocomotion.transform.localPosition = (Vector3.zero);
        LoadingSphere.SetActive(false);
        ResumeGame();
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void GotoMainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void CloseApp()
    {
        Application.Quit();
    }

}
