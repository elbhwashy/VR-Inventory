using Autohand;
using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private LocomotionManager locomotionManager;   
    [SerializeField] private TMPro.TMP_Dropdown movementDropdown; 

    public void OnMovementStatusChange(int value)
    {
        if(value == 0) // smooth
        {
            locomotionManager.ChangeLocomotion((LocomotionType.SmoothLocomotion) , true);
        }
        else if(value == 1) // Teleport
        {
            locomotionManager.ChangeLocomotion((LocomotionType.Teleport), true);

        }
    }
}
