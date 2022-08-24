using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolingSystem;

public class WandLogic : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenProjectiles = 3f;

    [Header("Mana Settings")]
    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaCost = 10f;

    private bool auraActive = false;
    private float startTime = 0f;

    private Material auraGlow;

    [SerializeField] private List<ParticleSystem> particleSystems;

    private void Awake()
    {
        auraGlow = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Start()
    {
        currentMana = maxMana;
        foreach (var system in particleSystems)
        {
            system.Stop();
        }
    }

    void Update()
    {
        if (EnteringAura())
        {
            auraActive = true;
            OnAuraEnter();
        }
        else if (ExitingAura())
        {
            auraActive = false;
            OnAuraExit();
        }
    }

    private void OnAuraExit()
    {
        auraGlow.SetFloat("_GlowIntensity", 1f);
        SpawnProjectile();
        auraGlow.SetFloat("_GlowIntensity", 0f);
        foreach (var system in particleSystems)
        {
            system.Stop();
        }
    }

    private void OnAuraEnter()
    {
        auraGlow.SetFloat("_GlowIntensity", 0.5f);
        currentMana -= manaCost;
        foreach (var system in particleSystems)
        {
            system.Play();
        }
    }

    private bool EnoughTimePassed()
    {
        return Time.time - startTime > timeBetweenProjectiles;
    }

    private void SpawnProjectile()
    {
        GameObject projectileInstance = ObjectPooler.SharedInstance.Instantiate(projectile, firePoint.position, firePoint.rotation);
        projectileInstance.GetComponent<Projectile>().SetSource(gameObject);
    }

    private bool StayingInAura()
    {
        return Input.GetMouseButtonDown(0) && auraActive;
    }

    private bool EnteringAura()
    {
        return Input.GetMouseButtonDown(0) && !auraActive && currentMana > 0;
    }

    private bool ExitingAura()
    {
        return Input.GetMouseButtonDown(1) && auraActive || currentMana < 0;
    }

    private void OnDisable()
    {
        auraGlow.SetFloat("_GlowIntensity", 0f);
    }

}
