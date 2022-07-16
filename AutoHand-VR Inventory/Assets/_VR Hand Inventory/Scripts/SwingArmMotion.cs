using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingArmMotion : MonoBehaviour
{
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject CenterEyeCamera;
    public GameObject ForwardDirection;


    private Vector3 PositionPreviousFrameLeftHand;
    private Vector3 PositionPreviousFrameRightHand;
    private Vector3 PlayerPositionPreviousFrame;
    private Vector3 PlayerPositionThisFrame;
    private Vector3 PositionThisFrameLeftHand;
    private Vector3 PositionThisFrameRightHand;


    public float Speed = 70;
    public float HandSpeed;

    // Start is called before the first frame update
    void Start()
    {

        PlayerPositionPreviousFrame = transform.position;
        PositionPreviousFrameLeftHand = LeftHand.transform.position;
        PositionPreviousFrameRightHand = RightHand.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = CenterEyeCamera.transform.eulerAngles.y;
        ForwardDirection.transform.eulerAngles = new Vector3(0f, yRotation, 0f);

        PositionThisFrameLeftHand = LeftHand.transform.position;
        PositionThisFrameRightHand = RightHand.transform.position;

        PlayerPositionThisFrame = transform.position;

        var playerDistanceMoved = Vector3.Distance(PlayerPositionThisFrame, PlayerPositionPreviousFrame);
        var leftHandDistanceMoved = Vector3.Distance(PositionPreviousFrameLeftHand, PositionThisFrameLeftHand);
        var rightHandDistanceMoved = Vector3.Distance(PositionPreviousFrameRightHand, PositionThisFrameRightHand);


        HandSpeed = ((leftHandDistanceMoved - playerDistanceMoved) + (rightHandDistanceMoved - playerDistanceMoved));
        
        if(Time.timeSinceLevelLoad > 1f)
        {
            transform.position += (ForwardDirection.transform.forward * HandSpeed * Speed * Time.deltaTime);
        }


        PositionPreviousFrameLeftHand = PositionThisFrameLeftHand;
        PositionPreviousFrameRightHand = PositionThisFrameRightHand;

        PlayerPositionPreviousFrame = PlayerPositionThisFrame;

    }
}
