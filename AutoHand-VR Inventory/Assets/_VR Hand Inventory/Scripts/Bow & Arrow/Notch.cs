using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Notch : MonoBehaviour
{
    [Header("Controller Reference")]
    public InputActionReference RightGrabNotchRefrenece = null;
    public InputActionReference LeftGrabNotchRefrenece = null;

    [Header("Hands")]
    public Transform rightHand;
    public Transform leftHand;

    [Header("Hands")]
    public Bow bow;
    public _String _String;

    [Header("Limits")]
    public Transform minLimit;    
    public Transform maxLimit;    

    [Header("Clone Arrow")]
    public GameObject currentArrow;
    [Range(0,1)]
    public float stretchForce;

    private bool IsRightHandIn = false;
    private bool IsRightGribPressed = false;


    private bool IsLeftHandIn = false;
    private bool IsLeftGribPressed = false;

    private void OnEnable()
    {
        // set up input refrenece
        RightGrabNotchRefrenece.action.started += HoldRightNotch; 
        RightGrabNotchRefrenece.action.canceled += RelaseRightNotch; 

        LeftGrabNotchRefrenece.action.started += HoldLeftNotch; 
        LeftGrabNotchRefrenece.action.canceled += RelaseLeftNotch; 
    }
    private void OnDisable()
    {
        RightGrabNotchRefrenece.action.started -= HoldRightNotch;
        RightGrabNotchRefrenece.action.canceled -= RelaseRightNotch; 

        LeftGrabNotchRefrenece.action.started -= HoldLeftNotch;
        LeftGrabNotchRefrenece.action.canceled -= RelaseLeftNotch;

    }


    private void Update()
    {
        if(IsRightHandIn && IsRightGribPressed && bow.bowStatus == _BowStatus.Filled)
        {
            StretchString(rightHand.position);

        }
        if (IsLeftHandIn && IsLeftGribPressed && bow.bowStatus == _BowStatus.Filled)
        {
            StretchString(leftHand.position);
        }


    }

    private void StretchString(Vector3 handPos)
    {
        Vector3 localTranform = transform.parent.InverseTransformPoint(handPos);

        float x = localTranform.x;
        float y = localTranform.y;
        float z = localTranform.z;

        if (z >= minLimit.localPosition.z)
        {
            z = minLimit.localPosition.z;
            SetStringStretching(z);

            _String.stringStatus = StringStatus.Idel;
        }
        else if (z <= maxLimit.localPosition.z)
        {
            z = maxLimit.localPosition.z;
            SetStringStretching(z);

            _String.stringStatus = StringStatus.Stretched;
        }
        else
        {
            SetStringStretching(z);

            _String.stringStatus = StringStatus.Stretched;
        }
    }

    private void SetStringStretching(float z)
    {
        _String.middle.localPosition = new Vector3(_String.middle.localPosition.x, _String.middle.localPosition.y, z);
        transform.localPosition = new Vector3(_String.middle.localPosition.x, _String.middle.localPosition.y, z);
        currentArrow.transform.localPosition = new Vector3(currentArrow.transform.localPosition.x, currentArrow.transform.localPosition.y, z);

        if (z >= minLimit.localPosition.z)
        {
            stretchForce = 0;
        }
        else
        {
            //stretchForce = force;
            stretchForce = (-1) * z;
        }
    }

    private void HoldRightNotch(InputAction.CallbackContext obj)
    { 
        if(IsRightHandIn)
        {
            IsRightGribPressed = true;
            _String.stringStatus = StringStatus.Stretched;
        }
    }

    private void HoldLeftNotch(InputAction.CallbackContext obj)
    {
        if (IsLeftHandIn)
        {
            IsLeftGribPressed = true;
            _String.stringStatus = StringStatus.Stretched;
        } 
    }

    private void RelaseRightNotch(InputAction.CallbackContext obj)
    { 
         IsRightGribPressed = false;
        if(_String.stringStatus == StringStatus.Stretched)
        {
            ResetBow();

            stretchForce = stretchForce * bow.BowForce;
            currentArrow.GetComponent<Arrow>().OnFireArrow(stretchForce);
        }
    }
    private void RelaseLeftNotch(InputAction.CallbackContext obj)
    {
        IsLeftGribPressed = false;
        if (_String.stringStatus == StringStatus.Stretched)
        {
            ResetBow();

            stretchForce = stretchForce * bow.BowForce;
            currentArrow.GetComponent<Arrow>().OnFireArrow(stretchForce);
        }
    }

    private void ResetBow()
    {
        SetNotchActivate(false);

        _String.stringStatus = StringStatus.Idel;
        bow.bowStatus = _BowStatus.Empty;

        _String.middle.localPosition = new Vector3(_String.middle.localPosition.x, _String.middle.localPosition.y, minLimit.localPosition.z);
        transform.localPosition = new Vector3(_String.middle.localPosition.x, _String.middle.localPosition.y, minLimit.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Cuatom Hand (XR)(L)")
        {
            IsLeftHandIn = true;
        }
        
        if(other.name == "Cuatom Hand (XR)(R)")
        {
            IsRightHandIn = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Cuatom Hand (XR)(R)")
        {
            IsRightHandIn = false;
        } 
        
        if(other.name == "Cuatom Hand (XR)(L)")
        {
            IsLeftHandIn = false;
        } 
    }

    public void SetNotchActivate(bool flag)
    {
        GetComponent<Collider>().enabled = flag;
    }

}
