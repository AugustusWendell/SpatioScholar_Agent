using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class AgentInit
{
    public string Type = "Patient";
    public string Block = "E";
    public string Ward = "4";
    public string ID = "Auto";
    public string Sex = "Female";
    public string State = "TBD";
    public int Total_Number = 12;
    public string HomeObject = "Home_ObjectName";

    public Dictionary<string, string> Itinerary = new Dictionary<string, string>()
    {
        { "110", "Target 1" },
        { "220", "Target 2" },
        { "330", "Target 1" }
    };

    public override string ToString()
    {
        string returnvalue = "yo";
        //returnvalue = testhours.ToString();
        returnvalue = "AgentInit ToString return = " + Type + " " + Block + "       dictionary = " + Itinerary.ToString();
        //returnvalue = testhours["key2"];
        return returnvalue;
    }

    public string ReturnTest()
    {
        return Itinerary["key2"].ToString();
    }

    public void ReturnDictionary()
    {
        foreach (string key in Itinerary.Keys)
        {
            string val = Itinerary[key];
            Debug.Log(key + " = " + val);
        }
    }
}