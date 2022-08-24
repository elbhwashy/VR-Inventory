using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorePlayerMana : MonoBehaviour
{

    ParticleSystem manaPotionParticleSystem;
    [SerializeField] private int restoreManaAmount = 10;
    public bool isIntheMouth = false;

    // Start is called before the first frame update
    void Start()
    {
        manaPotionParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //spill poor effect
        if (Vector3.Angle(Vector3.down, transform.forward) <= 90f)
        {
            manaPotionParticleSystem.Play();
            if (isIntheMouth)
            {
                StartCoroutine(RestoreHealth());
            }
        }
        else
        {
            manaPotionParticleSystem.Stop();
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
        FindObjectOfType<PlayerHealthMana>().HealPlayer(restoreManaAmount);
        Destroy(gameObject.transform.parent.gameObject);
    }

}
