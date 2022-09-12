using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementSpeedManager : MonoBehaviour
{
    public SmoothLocomotion smoothLocomotion;
    public UnityEngine.UI.Slider slider;

    public void OnSliderValueChanged(float value)
    {
        smoothLocomotion.MovementSpeed = value;
    }
}
