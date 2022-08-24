using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpillEffect : MonoBehaviour
{
    ParticleSystem healthPotionParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        healthPotionParticleSystem = transform.GetChild(1).GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Angle(Vector3.down, transform.forward) <= 90f)
        {
            healthPotionParticleSystem.Play();
        }
        else
        {
            healthPotionParticleSystem.Stop();
        }
    }
}
