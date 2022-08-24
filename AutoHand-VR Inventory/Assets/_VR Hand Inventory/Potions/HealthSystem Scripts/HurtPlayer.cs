using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//how much damage should do to the player

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;

    //checks for any objects that enter the trigger box
    //taht has this hurt script attach to it
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealthMana>().HurtPlayer(damageToGive);
        }
    }
}
