using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public GameObject loadingSphere;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeActivateLoadingScreen", 3.0f);
    }

    private void DeActivateLoadingScreen()
    {
        loadingSphere.SetActive(false);
    }
}
