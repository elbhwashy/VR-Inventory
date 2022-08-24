using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolingSystem;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform target;
    public GameObject projectile;
    public Transform firePoint;
    private bool shooting = false;

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            StartCoroutine(Shoot());
        }
        if (target)
            LookAtTarget();
    }

    private void LookAtTarget()
    {
        if(target != null)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z), Vector3.up);
        }
    }

    IEnumerator Shoot()
    {
        shooting = true;
        if(projectile != null)
        {
            GameObject ball = ObjectPooler.SharedInstance.Instantiate(projectile, firePoint.position, firePoint.rotation);
            ball.GetComponent<Projectile>().SetSource(gameObject);
        }
        yield return new WaitForSeconds(3f);
        shooting = false;

    }
}
