using System.Collections;
using UnityEngine;

public class MenuFollowPlayer : MonoBehaviour
{

	public Transform target; 
	public Vector3 targetRotation;
    public float speed = 2;

    void Update()
    {
        //StartCoroutine(LerpFunction(target.rotation, speed));
        //Vector3 targetRotation = new Vector3(0, UnityEditor.TransformUtils.GetInspectorRotation(target.transform).y, 0f);
        //StartCoroutine(LerpFunction(Quaternion.Euler(targetRotation), speed));


        StartCoroutine(LerpFunction(Quaternion.Euler(targetRotation), speed));

    }
    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
    }
}