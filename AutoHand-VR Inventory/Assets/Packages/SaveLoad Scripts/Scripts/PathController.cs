using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public static Action OnPathCreated;

    [SerializeField]
    private string folderName;

    public string RootPath { get; set; }

    private void Start()
    {
        SetRootPath();
    }

    private void SetRootPath()
    {
        RootPath = Application.persistentDataPath + "/" + folderName;

        if (!Directory.Exists(RootPath))
            Directory.CreateDirectory(RootPath);

        OnPathCreated?.Invoke();
    }
}
