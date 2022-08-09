using Autohand;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [Header("Type")]
    public _Type bowType;
    [Header("Status")]
    public _BowGrabStatus bowGrabStatus;
    public _BowStatus bowStatus;

    [Space]
    [Range(1,2)]
    public float BowForce = 1.0f;
    [Space]
    public Notch notch; 
    public Transform arrowPos;



    // Start is called before the first frame update
    void Start()
    {
        bowGrabStatus = _BowGrabStatus.Idle;
        bowStatus = _BowStatus.Empty;
        notch.SetNotchActivate(false);
    }


    public void OnGrabBow()
    {
        bowGrabStatus = _BowGrabStatus.Grabbed;
    }

    public void OnRelaseBow()
    {
        bowGrabStatus = _BowGrabStatus.Idle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Arrow>() != null)
        {
            Arrow arrow = collision.gameObject.GetComponent<Arrow>();
                //if (arrow.arrowType == bowType )
            if (arrow.arrowType == bowType && arrow.arrowStatus == _ArrowStatus.Grabbed && bowGrabStatus == _BowGrabStatus.Grabbed)
            {
                arrow.GetComponent<Grabbable>().HandsRelease();
                arrow.GetComponent<Grabbable>().enabled = false;                

                arrow.rb.isKinematic = true;
                arrow.col.isTrigger = true; 

                arrow.arrowStatus = _ArrowStatus.InBow;

                arrow.transform.parent = arrowPos.transform.parent.transform;
                arrow.transform.localPosition = arrowPos.localPosition;
                arrow.transform.localRotation = arrowPos.localRotation;

                notch.currentArrow = arrow.gameObject;

                notch.SetNotchActivate(true);

                bowStatus = _BowStatus.Filled;

            }

        }
    }

}

[System.Serializable]
public enum _BowGrabStatus
{
    Idle,
    Grabbed
}

[System.Serializable]
public enum _BowStatus
{
    Empty,
    Filled
}
