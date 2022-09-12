using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanleManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI infoText;


    public void OnClickItem(string info)
    {
        infoText.text = info;
    }

}
