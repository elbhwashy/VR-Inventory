using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneTransition : MonoBehaviour
{
    [SerializeField] private GameObject portalInstance;
    [SerializeField] private float portalMovementrate = 1f;
    [SerializeField] private string oldScene;
    [SerializeField] private string newScene;

    private bool reachedMiddle = false;
    private bool swappedScene = false;

    private void OnEnable()
    {
        SwapSceneAsync(oldScene, newScene);
    }

    private void FixedUpdate()
    {
        Debug.Log(reachedMiddle);
        if (portalInstance.transform.localPosition.z >= portalInstance.transform.localScale.z)
        {
            reachedMiddle = true;
        }
        if(!reachedMiddle)
        {
            portalInstance.transform.localPosition = new Vector3(portalInstance.transform.localPosition.x, portalInstance.transform.localPosition.y, portalInstance.transform.localPosition.z + portalMovementrate);
        }
        if(reachedMiddle && swappedScene)
        {
            portalInstance.transform.localPosition = new Vector3(portalInstance.transform.localPosition.x, portalInstance.transform.localPosition.y, portalInstance.transform.localPosition.z + portalMovementrate);
        }
        if(portalInstance.transform.localPosition.z > portalInstance.transform.localScale.z*2)
        {
            portalInstance.SetActive(false);
        }
    }

    public void SwapSceneAsync(string oldScene, string newScene)
    {
        swappedScene = false;
        reachedMiddle = false;
        portalInstance.transform.localPosition = Vector3.zero;
        portalInstance.SetActive(true);
        StartCoroutine(LoadYourAsyncScene(oldScene, newScene));
    }

    IEnumerator LoadYourAsyncScene(string oldScene, string newScene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        swappedScene = true;
        Debug.Log("Swapped scene");

        SceneManager.UnloadSceneAsync(oldScene);
    }
}
