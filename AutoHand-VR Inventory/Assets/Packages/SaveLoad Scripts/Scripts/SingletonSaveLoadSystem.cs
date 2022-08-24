using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonSaveLoadSystem : MonoBehaviour
{
    public static SingletonSaveLoadSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}
