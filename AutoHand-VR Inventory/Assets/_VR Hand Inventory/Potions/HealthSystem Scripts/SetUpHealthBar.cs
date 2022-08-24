using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetUpHealthBar : MonoBehaviour
{
    public Slider playerHealthSlider = null;

    public void SetMaxHealth(int health)
    {
        playerHealthSlider.maxValue = health;
        playerHealthSlider.value = health;
    }
    public void SetHealth(int health)
    {
        playerHealthSlider.value = health;
    }
}
