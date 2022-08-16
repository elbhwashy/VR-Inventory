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
        BNG.Arrow arrow = collision.gameObject.GetComponent<BNG.Arrow>();
        if (arrow != null)
        {
            //if (targetType == arrow.arrowType)
            {
                SetHealth(arrow);
            }
        }
    }


    IEnumerator SetArrowDeActive(BNG.Arrow arrow)
    {
        arrow.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
       
    }

    private void SetHealth(BNG.Arrow arrow)
    {
        health -= arrow.ProjectileObject.Damage;

        healthBar.fillAmount = health / maxHealth;
    }
}
