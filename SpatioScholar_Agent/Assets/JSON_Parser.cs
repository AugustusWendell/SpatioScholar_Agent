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
    private AgentInit loadedData;
    public SScholar_Agent_Controller Controller;

    void Start()
    {

        //Output the Game data path to the console
        //Debug.Log(Application.dataPath);
        //LoadGameData();
        LoadGameData(gameDataFileName);
        RunInit();
        LoadGameData(gameDataFileName2);
        RunInit();
    }

    void RunInit()
    {
        //(loadedData.Total_Number -3)
        for (int i = 1; i < loadedData.Total_Number; i++)
        {
            //Controller.AddAgent();
            Controller.Initialize_Agent(loadedData);
        }
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

            Debug.Log("loadedData as deserialized string direct from imported file text = "  + dataAsJson);


            /*
            //Unity JSON library
            // Pass the json to JsonUtility, and tell it to create a AgentInit object from it
            AgentInit loadedData = JsonUtility.FromJson<AgentInit>(dataAsJson);
            */

            //UltimateJSON library - in an attempt to deserialize a dictionary in the Json, meaning unstructured data
            loadedData = UltimateJson.JsonObject.Deserialise<AgentInit>(dataAsJson);


            Debug.Log("Json cast as object into a ToString function   = " + loadedData.ToString());
            loadedData.ReturnDictionary();

            
            //write out a test json output to verify using UltimateJSON
            string jsonString = UltimateJson.JsonObject.Serialise(loadedData, false);
            string filePath2 = Path.Combine(Application.dataPath, saveDataFileName);
            File.WriteAllText(filePath2, jsonString);
            
        }
        else
        {
            Debug.LogError("Cannot load json AgentInit object!");
        }
    }

    private void LoadGameData(string filepath_string)
    {
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.dataPath, filepath_string);

        //Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            //Debug.Log("loadedData as deserialized string direct from imported file text = " + dataAsJson);

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
