using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public _Type targetType;

    [Space]
    public float health = 30;
    private float maxHealth;

    [Space]
    public Image healthBar;


    private void Start()
    {
        maxHealth = health;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Arrow arrow = collision.gameObject.GetComponent<Arrow>();
        if (arrow != null)
        {
            if (targetType == arrow.arrowType)
            {
                OnArrowEnterTarget(arrow);
                SetHealth(arrow);
            }
        }
    }

    private void OnArrowEnterTarget(Arrow arrow)
    {
        arrow.arrowStatus = _ArrowStatus.Hit;

        arrow.transform.parent = transform;
        arrow.rb.velocity = Vector3.zero;
        arrow.rb.angularVelocity = Vector3.zero;
        arrow.col.isTrigger = true;
        arrow.rb.isKinematic = true;

        Destroy(arrow.grabbable);
    }

    private void SetHealth(Arrow arrow)
    {
        health -= arrow.arrowDamage;

        healthBar.fillAmount = health / maxHealth;
    }
}
