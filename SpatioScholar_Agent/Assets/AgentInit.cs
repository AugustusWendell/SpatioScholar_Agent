using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[System.Serializable]
public class AgentInit
{
    public string type;
    public string itinerary;
    public Dictionary<string, string> testhours;

    public override string ToString()
    {
        string returnvalue = "yo";
        //returnvalue = testhours.ToString();
        returnvalue = type + itinerary;
        //returnvalue = testhours["key2"];
        return returnvalue;
    }

    public string ReturnTest()
    {
        return testhours["key2"];
    }
}