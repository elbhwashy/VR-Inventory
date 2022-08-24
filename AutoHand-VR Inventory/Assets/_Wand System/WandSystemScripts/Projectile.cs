using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifespan = 3f;
    public float damage = 3f;
    private Rigidbody rigidBody;

    private GameObject source;

    private float _enableTime;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        source = gameObject;
        _enableTime = Time.time;
    }

    void Update()
    {
        if(speed > 0)
        {
            rigidBody.velocity = rigidBody.transform.forward * speed;
        }
        if (Time.time - _enableTime > lifespan)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.TryGetComponent(typeof(IDamageable<float>), out Component damageable)) //If the enter object can be damaged, do damage
        {
            damageable.gameObject.GetComponent<IDamageable<float>>().Damage(damage);
        }
        gameObject.SetActive(false);

    }

    public void SetSource(GameObject Source)
    {
        source = Source;
    }

    public GameObject GetSource()
    {
        return source;
    }
}
