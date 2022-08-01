using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private AutoHandPlayer autoHandPlayer; 
    [SerializeField] private Teleporter playerTeleport; 
    [SerializeField] private TMPro.TMP_Dropdown movementDropdown; 


    public void OnMovementStatusChange(int value)
    {
        if(value == 0) // smooth
        {
            autoHandPlayer.useMovement = true;
            playerTeleport.enabled = false;
        }
        else if(value == 1) // Teleport
        {
            autoHandPlayer.useMovement = false;
            playerTeleport.enabled = true;
        }
    }
}
