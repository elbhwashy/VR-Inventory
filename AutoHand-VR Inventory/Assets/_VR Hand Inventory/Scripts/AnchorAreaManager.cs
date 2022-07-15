using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnchorAreaManager : MonoBehaviour
{
    public AutoHandPlayer autoHandPlayer;

    public GameObject anchor;
    public GameObject portal;
    public string nextSceneName;
    public float loadingTime = 5.0f;

    private void Awake()
    {
        portal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + " jghj " + other.name);
        if(other.tag == "Player")
        {
            // Activate the portal & then move to the next scene
            anchor.SetActive(false);
            portal.gameObject.SetActive(true);
            autoHandPlayer.enabled = false;


            Invoke("MoveToTriggerScene", loadingTime);
        }
    }
     

    private void MoveToTriggerScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
