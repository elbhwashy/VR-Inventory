using Autohand;
using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class RotateManager : MonoBehaviour
{
    [SerializeField] private PlayerRotation playerRotation;  
    [SerializeField] private TMPro.TMP_Dropdown movementDropdown; 


    public void OnRotateStatusChange(int value)
    {
        if(value == 0) // smooth
        {
            playerRotation.RotationType = RotationMechanic.Smooth;
        }
        else if(value == 1) // snap
        {
            playerRotation.RotationType = RotationMechanic.Snap;
        }
    }
}
