using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class SScholar_Agent_Controller : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_Target1Button, m_Target2Button, m_AddAgentButton, m_ToggleVectorButton, m_CSV_SaveButton, m_DebugRaysButton;
    public Toggle IntervisibilityToggle;
    public GameObject controller;
    public GameObject target1;
    public GameObject target2;
    public GameObject spawnpoint;
    public NavMeshAgent SSAgent;
    public Canvas SS_Agent_Canvas;

    //for use changing variables
    GameObject referenceObject;
    PlayerController referenceScript;

    //Trying to make the agent list specific to NavMeshAgents so we can call specific NavMeshAgent methods from them.
    public List<NavMeshAgent> AgentList = new List<NavMeshAgent>();

    //more generic List containing only GameObjects rather than more specific NavMeshAgents
    //public List<GameObject> AgentList = new List<GameObject>();

    //List of GameObjects used for intervisibility testing. Realistically, this will often be more than one object
    public List<GameObject> IntervisibilityTargets = new List<GameObject>();

    void Start()
    {
        m_Target1Button.onClick.AddListener(delegate { UpdateTarget1(); });
        m_Target2Button.onClick.AddListener(delegate { UpdateTarget2(); });
        m_AddAgentButton.onClick.AddListener(delegate { AddAgent(); });
        m_ToggleVectorButton.onClick.AddListener(delegate { ToggleVector(); });

        //trying this out......to get the intervisibility toggle to work
        IntervisibilityToggle.onValueChanged.AddListener(delegate { ToggleIntervisibility(); });

        m_CSV_SaveButton.onClick.AddListener(delegate {
            //GetComponent<CSV_output>().Save();
            SaveCSV();
        });
        m_DebugRaysButton.onClick.AddListener(delegate { ToggleDebugRays(); });

        //set/populate controller and target objects
        controller = GameObject.Find("SScholar_Agent_Controller");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");

        List<NavMeshAgent> AgentList = new List<NavMeshAgent>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print("Tab key was pressed, Toggle Main UI");
            ToggleUI();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            print("A key was pressed, Adding new Agent");
            AddAgent();
        }
    }

    void TaskOnClick()
    {
        //Output this to console when the Button is clicked
        Debug.Log("You have clicked the button!");
    }


    void ToggleUI()
    {
        print("ToggleUI method called");
        if (SS_Agent_Canvas.enabled)
        {
            SS_Agent_Canvas.enabled = false;
        }
        else
        {
            SS_Agent_Canvas.enabled = true;
        }
    
    }

    void SaveCSV()
    {
        GetComponent<CSV_output>().Save();
    }

    void TaskWithParameters(string message)
    {
        //Output this to console when the Button is clicked
        Debug.Log(message);
    }


	void UpdateTarget1()
	{
        try
        {
            for (int i = 0; i < AgentList.Count; i++)
            {
                AgentList[i].SetDestination(target1.transform.position);
            }
        }
        catch (Exception e)
        {
            print("error");
        }
    }

    

       void ToggleDebugRays()
        {
        try
        {
            for (int i = 0; i < AgentList.Count; i++)
            {
                AgentList[i].GetComponent<PlayerController>().ToggleDebugRays();
            }
        }
        catch (Exception e)
        {
            print("error");
        }
    }

    void AddAgent()
    {
        //Default Start Location for new agents - this ough to be controlled through a more sophisticated method!
        //Vector3 AddAgentLocation = new Vector3(1.0f, 1.0f, 1.0f);

        //samos spawn location
        //Vector3 AddAgentLocation = new Vector3(-350.0f, 16.0f, -288.0f);

        //SSA Spawn Locator 
        Vector3 AddAgentLocation = spawnpoint.transform.position;

        Quaternion AddAgentRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        NavMeshAgent newAgent = (NavMeshAgent)Instantiate(SSAgent, AddAgentLocation, AddAgentRotation);
        AgentList.Add(newAgent);
        for (int i = 0; i < AgentList.Count; i++)
            {
            print("AgentList List object count number " + i);
            print(AgentList[i]);
        }
        }


    void UpdateTarget2()
    {

        try
        {
            for (int i = 0; i < AgentList.Count; i++)
            {
                AgentList[i].SetDestination(target2.transform.position);
            }
        }
        catch (Exception e)
        {
            print("error");
        }
       
    }

    //Method to return IntervisibilityTargets
    public List<GameObject> GetIntervisibilityTargets()
    {
        return IntervisibilityTargets;
    }

    //function to add a GameObject to the Controller Intervisibility Target List
    //This should function should be called from each instance of the Intervisibility Target Scripts Start() method.
    public void AddIntervisibilityTarget(GameObject target)
    {
        IntervisibilityTargets.Add(target);
        Debug.Log("Intervisibility target " + target.name + " added");
    }

    void ToggleIntervisibility()
    {
        try
        {
            for (int i = 0; i < AgentList.Count; i++)
            {
                NavMeshAgent t = AgentList[i];
                referenceObject = t.gameObject;
                referenceScript = referenceObject.GetComponent<PlayerController>();
                referenceScript.ToggleIntervisibility();
            }
        }
        catch (Exception e)
        {
            print("error");
        }
    }

    void ToggleVector()
    {

        try
        {
            for (int i = 0; i < AgentList.Count; i++)
            {
                NavMeshAgent t = AgentList[i];
                referenceObject = t.gameObject;
                referenceScript = referenceObject.GetComponent<PlayerController>();
                referenceScript.ToggleVector();
            }
        }
        catch (Exception e)
        {
            print("error");
        }
    }

}
