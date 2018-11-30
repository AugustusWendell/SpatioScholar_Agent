using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files

public class JSON_Parser : MonoBehaviour
{

    private string gameDataFileName = "data.json";
    private string saveDataFileName = "data2.json";

    void Start()
    {

        //Output the Game data path to the console
        Debug.Log(Application.dataPath);

        LoadGameData();
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        //Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);

            Debug.Log(dataAsJson);

            // Pass the json to JsonUtility, and tell it to create a AgentInit object from it
            AgentInit loadedData = JsonUtility.FromJson<AgentInit>(dataAsJson);

            Debug.Log(loadedData.ToString());
            //Debug.Log(loadedData.ReturnTest());

            string jsonString = JsonUtility.ToJson(loadedData);
            string filePath2 = Path.Combine(Application.dataPath, saveDataFileName);
            File.WriteAllText(filePath2, jsonString);

        }
        else
        {
            Debug.LogError("Cannot load json AgentInit object!");
        }
    }

}
