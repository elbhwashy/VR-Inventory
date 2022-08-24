using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealthMana : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxMana = 100;

    public int currentHealth;
    [SerializeField] private float currentMana;

    public SetUpHealthBar setUpHealthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        setUpHealthBar.SetMaxHealth(maxHealth);
    }


    public void HurtPlayer(int damage)
    {
        //take away our damage value from whatever 
        //our current health is in the game
        currentHealth -= damage;
        setUpHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            
            //Respawn();
        }
    }

    public void HealPlayer(int HealAmount)
    {
        //dont go higher than the max health is
        currentHealth += HealAmount;
        setUpHealthBar.SetHealth(currentHealth);

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    
    

}
