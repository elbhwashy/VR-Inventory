using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    string saveFile;

    GameData gameData = new GameData();

    void Awake()
    {
        // Update the field once the persistent path exists.
        saveFile = Application.persistentDataPath + "/gamedata.json";
    }

    public void readFile()
    {
        // Does the file exist?
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);

            // Work with JSON
        }
    }

    public void writeFile()
    {
        //File.WriteAllText(saveFile, jsonString);

        // Work with JSON
    }
}
