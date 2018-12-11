using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;                                                        // The System.IO namespace contains functions related to loading and saving files
using MARECEK;
using UltimateJson;

public class JSON_Parser : MonoBehaviour
{

    private string gameDataFileName = "data.json";
    private string gameDataFileName2 = "data2.json";
    private string saveDataFileName = "data3.json";
    private string subfolder = "Sscholar_Agent_inits/";
    private AgentInit loadedData;
    public SScholar_Agent_Controller Controller;

    void Start()
    {
        LoadGameData(gameDataFileName);
        RunInit();
        LoadGameData(gameDataFileName2);
        RunInit();
    }

    void RunInit()
    {
        for (int i = 0; i < loadedData.Total_Number; i++)
        {
            Debug.Log("initializing 1 agent");
            //this passes the integer number along with the call, this could be useful in determining which child object to use as the true home object
            Controller.Initialize_Agent(loadedData, i);
        }
        //make sure the AgentInit variable is empty
        loadedData = null;
    }

    private void LoadGameData(string filepath_string)
    {
        //make sure the AgentInit variable is empty
        loadedData = null;
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.dataPath, subfolder);
        filePath = Path.Combine(filePath, filepath_string);
        Debug.Log("Looking for an Agent Init file in " + filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log("loadedData as deserialized string direct from imported file text = " + dataAsJson);

            //UltimateJSON library - in an attempt to deserialize a dictionary in the Json, meaning unstructured data
            loadedData = UltimateJson.JsonObject.Deserialise<AgentInit>(dataAsJson);

            //Debug.Log("Json cast as object into a ToString function   = " + loadedData.ToString());
            loadedData.ReturnDictionary();
        }
        else
        {
            Debug.LogError("Cannot load json AgentInit object!    for filename  =  " + filepath_string);
        }
    }

}
