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
    private Item item;
    private bool isTriggerClicked = false;

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
            if (!system.isPlaying) system.Stop();
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
        if (isTriggerClicked)
        {
            timer += Time.deltaTime;

            foreach (var system in auraParticals)
            {
                if(!system.isPlaying) system.Play();
            }

            if (timer >= 2.0f)
            {
                Debug.Log("Fire Projectile  :) ");
                timer = 0.0f;
                SpawnProjectile();
            }
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectileInstance = ObjectPooler.SharedInstance.Instantiate(projectile, firePoint.position, firePoint.rotation);
        //projectileInstance.GetComponent<Projectile>().SetSource(this.gameObject);
    }

}
