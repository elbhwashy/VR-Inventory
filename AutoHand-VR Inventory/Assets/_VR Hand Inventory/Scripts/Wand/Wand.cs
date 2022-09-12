using BNG;
using ObjectPoolingSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    [Header("toggle Controller References")]
    public InputActionReference rightTriggerButton_Refrenece = null;
    [SerializeField] private ParticleSystem []auraParticals; 
    [SerializeField] private GameObject projectile; 
    [SerializeField] private Transform firePoint;
    [SerializeField] private int manaConsumption = 5;

    public Grabber rightGrabber;
    public ManaBar manaBar;
    private GrabbableHaptics grabbableHaptics;

    private Item item;
    private bool isTriggerClicked = false;
    private bool haveMana = true;

    private void Awake()
    {
        grabbableHaptics = GetComponent<GrabbableHaptics>();
    }

    private void Start()
    {
        item = GetComponent<Item>();
    }
    private void OnEnable()
    {
        // set up input refrenece
        rightTriggerButton_Refrenece.action.started += OnRightTriggerStarted;
        rightTriggerButton_Refrenece.action.canceled += OnRightTriggerCanceled;     
    }

    private void OnRightTriggerStarted(InputAction.CallbackContext obj)
    {
       if(item.itemStatus == ItemStatus.Grabbed)
       {
           isTriggerClicked = true;
       }
    }

    private void OnRightTriggerCanceled(InputAction.CallbackContext obj)
    {
        isTriggerClicked = false;
        timer = 0;
        foreach (var system in auraParticals)
        {
            system.Stop();
        }
    }

    private void OnDisable()
    {
        rightTriggerButton_Refrenece.action.started -= OnRightTriggerStarted;
        rightTriggerButton_Refrenece.action.canceled -= OnRightTriggerCanceled; 
    }


    float timer = 0;
    private void Update()
    {
        if(PlayerInfo._Mana >= manaConsumption) haveMana = true;

        if (isTriggerClicked && haveMana)
        {
            timer += Time.deltaTime;

            foreach (var system in auraParticals)
            {
                if(!system.isPlaying) system.Play();
            }

            if (timer >= 2.0f)
            {
                if(PlayerInfo._Mana >= manaConsumption)
                {
                    Debug.Log("Fire Projectile  :) ");
                    timer = 0.0f;
                    SpawnProjectile();
                    ConsumMana();
                }
                else
                {
                    timer = 0.0f;
                    haveMana = false;
                    foreach (var system in auraParticals)
                    {
                        system.Stop();
                    }
                }
            }
        }
    }

    private void ConsumMana()
    {
        PlayerInfo._Mana -= manaConsumption;

        manaBar.UpdateManaBar();
    }

    private void SpawnProjectile()
    {
        GameObject projectileInstance = ObjectPooler.SharedInstance.Instantiate(projectile, firePoint.position, firePoint.rotation);
        grabbableHaptics.doHaptics(rightGrabber.HandSide);
    }
    
}
