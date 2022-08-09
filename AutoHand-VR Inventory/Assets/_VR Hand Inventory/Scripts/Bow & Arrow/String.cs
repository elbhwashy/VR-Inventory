using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class String : MonoBehaviour
{
    public LineRenderer lineRenderer;

    [Space]
    public StringStatus stringStatus;

    [Space]
    public Transform start;
    public Transform middle;
    public Transform end;

    private void Start()
    {
        stringStatus = StringStatus.Idel;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, start.position);        
        lineRenderer.SetPosition(1, middle.position);        
        lineRenderer.SetPosition(2, end.position);        
    }
}

public enum StringStatus
{
    Idel,
    Stretched
}
