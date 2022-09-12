using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Image fillAreaImage;
    public TMPro.TextMeshProUGUI fillAreaText;

    private void Start()
    {
        UpdateManaBar();
    }

    public void UpdateManaBar()
    {        
        fillAreaImage.fillAmount = (PlayerInfo._Mana / PlayerInfo._MaxMana);
        fillAreaText.text = ((PlayerInfo._Mana / PlayerInfo._MaxMana) * 100 ) + " %";
    }

}
