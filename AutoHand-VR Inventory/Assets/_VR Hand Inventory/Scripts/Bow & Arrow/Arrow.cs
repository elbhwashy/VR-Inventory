using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Space]
    public _Type arrowType;
    public _ArrowStatus arrowStatus; 

    [Space]
    public Grabbable grabbable;
    public Rigidbody rb;
    public Collider col;
    [Space]
    public float arrowDamage = 10;
    [SerializeField] private float OnFireTimer = 3.0f;

    private Transform arrowInitialParent;
    private Vector3 arrowInitialPos;
    private Quaternion arrowInitialRot;




    public void OnGrabArrow()
    {
        arrowStatus = _ArrowStatus.Grabbed;
        rb.isKinematic = false;
    }

    public void OnRelaseArrow()
    {
        arrowStatus = _ArrowStatus.Idle;
        rb.isKinematic = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        arrowInitialPos = this.transform.localPosition;
        arrowInitialRot = this.transform.localRotation;
        arrowInitialParent = transform.parent;

        arrowStatus = _ArrowStatus.Idle;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if(arrowStatus == _ArrowStatus.Fired)
        {
            OnFireTimer -= Time.deltaTime;
            if (OnFireTimer <= 0.0)
            {
                ResetArrow();
            }
        }
    }

    private void ResetArrow()
    {
        transform.parent = arrowInitialParent;
        transform.localPosition = arrowInitialPos;
        transform.localRotation = arrowInitialRot;

        arrowStatus = _ArrowStatus.Idle;
        GetComponent<Grabbable>().enabled = true;

        rb.useGravity = false;
        rb.isKinematic = true;

        col.isTrigger = false;
        OnFireTimer = 3.0f;
    }  

    public void OnFireArrow(float force)    
    {
        transform.parent = null; 

        arrowStatus = _ArrowStatus.Fired;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force * 5000f);

        Invoke("SetColliderDeActive", 0.1f);
    }

    public void SetColliderDeActive()
    {
        col.isTrigger = false;
    }

}


[System.Serializable]
public enum _Type
{
    Egyptian,
    Volcanic,
    Holy,
    Ice,
    Forest,
    Normal,
    Water
}

[System.Serializable]
public enum _ArrowStatus
{
    Idle,
    Grabbed,
    InBow,
    Fired,
    Hit
}
