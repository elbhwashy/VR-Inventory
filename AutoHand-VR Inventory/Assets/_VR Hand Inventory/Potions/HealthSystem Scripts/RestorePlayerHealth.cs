using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePlayerHealth : MonoBehaviour
{
    ParticleSystem healthPotionParticleSystem;
    [SerializeField] private int restoreHealthAmount = 10;
    public bool isIntheMouth = false;

    // Start is called before the first frame update
    void Start()
    {
        healthPotionParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //spill poor effect
        if (Vector3.Angle(Vector3.down, transform.forward) <= 90f)
        {
            healthPotionParticleSystem.Play();
            if (isIntheMouth)
            {
                //healthPotionParticleSystem.Play();
                StartCoroutine(RestoreHealth());
            }
        }
        else
        {
            healthPotionParticleSystem.Stop();
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            isIntheMouth = true;
        }

    }

    private IEnumerator RestoreHealth()
    {
        yield return new WaitForSeconds(2);
        //Heal();
        FindObjectOfType<PlayerHealthMana>().HealPlayer(restoreHealthAmount);
        Destroy(gameObject.transform.parent.gameObject);
    }

     /*private void Heal()
     {
         //currentHealth += HealAmount;
         playerHealthSlider.value += restoreHealthAmount;

         //playerHealthSlider.SetHealth(player);
     }*/



    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealthMana>().HealPlayer(restoreHealthAmount);
        }
    }*/
}
