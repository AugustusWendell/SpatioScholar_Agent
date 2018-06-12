using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.AI;

public class CSV_output : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();


    // Use this for initialization
    void Start()
    {
        //Calls the Save() method to save out the CSV file
        //Save();
    }

    public void Save()
    {
        print("CSV Save Method Called");
        // Creating First row of titles manually.
        string[] rowDataTemp = new string[5];
        rowDataTemp[0] = "ID_Number";
        rowDataTemp[1] = "Birth_Location";
        rowDataTemp[2] = "Current_Location";
        rowDataTemp[3] = "Current_Vector";
        rowDataTemp[4] = "Target";
        rowData.Add(rowDataTemp);


        /* example calls to retrieve variables and call methods in other components added to the same object
        GetComponent(Gravity).SetGravityRange(1000);
        GetComponent<Example>().AgentList;
        */

        // You can add up the values in as many cells as you want.
        //for (int i = 0; i < GetComponent<Example>().AgentList.Count; i++)
        for (int i = 1; i < 3; i++)
        {
            //debug
            print("CSV Save data method : finding agent number " + i);
            //debug object being queried
            print("CSV Save data method : finding information from agent " + GetComponent<Example>().AgentList[i]);
            rowDataTemp = new string[5];
            //rowDataTemp[0] = "Sushanta" + i; // name
            rowDataTemp[0] = "" + i; // ID_Number
            rowDataTemp[1] = GetComponent<Example>().AgentList[i].transform.position.ToString(); // Birth_Location
            rowDataTemp[2] = GetComponent<Example>().AgentList[i].transform.position.ToString(); // Current_Location
            rowDataTemp[3] = GetComponent<Example>().AgentList[i].velocity.ToString(); // Current_Vector
            rowDataTemp[4] = GetComponent<Example>().AgentList[i].destination.ToString(); // Target
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }


   
    

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}

