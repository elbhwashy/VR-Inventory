//
//
//NOTES:
//
//This script is used for DEMONSTRATION porpuses of the Projectiles. I recommend everyone to create their own code for their own projects.
//THIS IS JUST A BASIC EXAMPLE PUT TOGETHER TO DEMONSTRATE VFX ASSETS.
//
//




#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPoolingSystem;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileMoveScript : MonoBehaviour {

    public bool rotate = false;
    public float rotateAmount = 45;
    public bool bounce = false;
    public float bounceForce = 10;
    public float speed;
	[Tooltip("From 0% to 100%")]
	public float accuracy;
	public float fireRate;
	public GameObject muzzlePrefab;
	public GameObject hitPrefab;
	public List<GameObject> trails;

    private Vector3 startPos;
	private float speedRandomness;
	private Vector3 offset;
	private bool collided;
	private Rigidbody rb;
    private RotateToMouseScript rotateToMouse;
    private GameObject target;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable ()
    {
        collided = false;
        startPos = transform.position;
        rb.isKinematic = false;

        CalculateOffsetBasedOnAccuracy();

        if (muzzlePrefab != null)
        {
            var muzzleVFX = ObjectPooler.SharedInstance.Instantiate(muzzlePrefab, transform.position, transform.rotation);
            var ps = muzzleVFX.GetComponent<ParticleSystem>();
            if (ps != null)
                muzzleVFX.GetComponent<ReturnToPool>().ReturnAfter(ps.main.duration);
            else
            {
                var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                muzzleVFX.GetComponent<ReturnToPool>().ReturnAfter(psChild.main.duration);
            }
        }
    }

    private void CalculateOffsetBasedOnAccuracy()
    {
        //used to create a radius for the accuracy and have a very unique randomness
        if (accuracy != 100)
        {
            accuracy = 1 - (accuracy / 100);

            for (int i = 0; i < 2; i++)
            {
                var val = 1 * Random.Range(-accuracy, accuracy);
                var index = Random.Range(0, 2);
                if (i == 0)
                {
                    if (index == 0)
                        offset = new Vector3(0, -val, 0);
                    else
                        offset = new Vector3(0, val, 0);
                }
                else
                {
                    if (index == 0)
                        offset = new Vector3(0, offset.y, -val);
                    else
                        offset = new Vector3(0, offset.y, val);
                }
            }
        }
    }

    void FixedUpdate () {

        if (target != null)
            rotateToMouse.RotateToMouse (gameObject, target.transform.position);
        if (rotate)
            transform.Rotate(0, 0, rotateAmount, Space.Self);
        if (speed != 0 && !collided)
            rb.velocity = rb.transform.forward * speed;
    }

	void OnCollisionEnter (Collision co) {
        if (!bounce)
        {
            if (co.gameObject.tag != "Bullet" && !collided && co.gameObject!=GetComponent<Projectile>().GetSource())
            {
                collided = true;

                rb.isKinematic = true;

                ContactPoint contact = co.contacts[0];
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;

                if (hitPrefab != null)
                {
                    var hitVFX = ObjectPooler.SharedInstance.Instantiate(hitPrefab, pos, rot) as GameObject;

                    var ps = hitVFX.GetComponent<ParticleSystem>();
                    if (ps == null)
                    {
                        var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                        hitVFX.GetComponent<ReturnToPool>().ReturnAfter(psChild.main.duration);
                    }
                    else
                        hitVFX.GetComponent<ReturnToPool>().ReturnAfter(ps.main.duration);
                }

                StartCoroutine(DestroyParticle(0f));
            }
        }
        else
        {
            rb.useGravity = true;
            rb.drag = 0.5f;
            ContactPoint contact = co.contacts[0];
            rb.AddForce (Vector3.Reflect((contact.point - startPos).normalized, contact.normal) * bounceForce, ForceMode.Impulse);
            gameObject.SetActive(false);
        }
	}

	public IEnumerator DestroyParticle (float waitTime) {

		if (transform.childCount > 0 && waitTime != 0) {
			List<Transform> tList = new List<Transform> ();

			foreach (Transform t in transform.GetChild(0).transform) {
				tList.Add (t);
			}		

			while (transform.GetChild(0).localScale.x > 0) {
				yield return new WaitForSeconds (0.01f);
				transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				for (int i = 0; i < tList.Count; i++) {
					tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
				}
			}
		}
		
		yield return new WaitForSeconds (waitTime);
        gameObject.SetActive(false);
    }

    public void SetTarget (GameObject trg, RotateToMouseScript rotateTo)
    {
        target = trg;
        rotateToMouse = rotateTo;
    }
}
