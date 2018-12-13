using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class AgentInit
{
    public string Type;
    public string Block;
    public string Ward;
    public string ID;
    public string Sex;
    public string State;
    public int Total_Number;
    public string HomeObject;
    public string Color;

    public Dictionary<string, string> Itinerary;

    public override string ToString()
    {
        string returnvalue = "yo";
        //returnvalue = testhours.ToString();
        //returnvalue = "AgentInit ToString return = " + Type + " " + Block + "       dictionary = " + Itinerary.ToString();
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
            //Debug.Log(key + " = " + val);
        }
    }
}