using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject loadingSphere;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeActivateLoadingScreen", 3.0f);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private void DeActivateLoadingScreen()
    {
        loadingSphere.SetActive(false);
    }
}
