using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private AutoHandPlayer autoHandPlayer;  
    [SerializeField] private TMPro.TMP_Dropdown movementDropdown; 


    public void OnRotateStatusChange(int value)
    {
        if(value == 0) // smooth
        {
            autoHandPlayer.snapTurning = false;
            
        }
        else if(value == 1) // snap
        {
            autoHandPlayer.snapTurning = true;
        }
    }
}
