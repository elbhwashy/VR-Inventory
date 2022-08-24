using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetUpManaBar : MonoBehaviour
{
    public Slider playerManaSlider = null;

    public void SetMaxMana(int mana)
    {
        playerManaSlider.maxValue = mana;
        playerManaSlider.value = mana;
    }
    public void SetMana(int mana)
    {
        playerManaSlider.value = mana;
    }

}
