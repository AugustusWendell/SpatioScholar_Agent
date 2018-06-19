using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;


public class Example : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector
    public Button m_Target1Button, m_Target2Button, m_AddAgentButton, m_ToggleVectorButton, m_CSV_SaveButton, m_DebugRaysButton;
    public GameObject controller;
    public GameObject target1;
    public GameObject target2;
    //deprecated
    //public NavMeshAgent Agent1;
    //public NavMeshAgent Agent2;
    public NavMeshAgent SSAgent;
    public Canvas SS_Agent_Canvas;

    //for use changing variables
    GameObject referenceObject;
    PlayerController referenceScript;

    //Trying to make the agent list specific to NavMeshAgents so we can call specific NavMeshAgent methods from them.
    public List<NavMeshAgent> AgentList = new List<NavMeshAgent>();

    //more generic List containing only GameObjects rather than more specific NavMeshAgents
    //public List<GameObject> AgentList = new List<GameObject>();

    void Start()
    {
        //Button btn = m_Target1Button.GetComponent<Button>();
        //Button btn2 = m_Target2Button.GetComponent<Button>();

        //Calls the TaskOnClick method when you click the Button
        //btn.onClick.AddListener(TaskOnClick);

        //example listener assignment
        //m_Target1Button.onClick.AddListener(delegate { TaskWithParameters("Hello"); });

        m_Target1Button.onClick.AddListener(delegate { UpdateTarget1(); });
        m_Target2Button.onClick.AddListener(delegate { UpdateTarget2(); });
        m_AddAgentButton.onClick.AddListener(delegate { AddAgent(); });
        m_ToggleVectorButton.onClick.AddListener(delegate { ToggleVector(); });
        m_CSV_SaveButton.onClick.AddListener(delegate {
            //GetComponent<CSV_output>().Save();
            SaveCSV();
        });
        m_DebugRaysButton.onClick.AddListener(delegate { ToggleDebugRays(); });



        //set/populate controller and target objects
        controller = GameObject.Find("SS_Agent_Controller");
        target1 = GameObject.Find("Target 1");
        target2 = GameObject.Find("Target 2");

        List<NavMeshAgent> AgentList = new List<NavMeshAgent>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print("tab key was pressed");
            ToggleUI();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
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
        if (SS_Agent_Canvas.enabled == true)
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
        Vector3 AddAgentLocation = new Vector3(1.0f, 1.0f, 1.0f);
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
               
                
                /*
                // Now in the start method, you need to get the references of these things inside a scene.
                // To find the GameObject with tag "ObjectOne" you would use:
                referenceObject = GameObject.FindObjectWithTag("ObjectOne");
// this method searches the scene for object, which is tagged "ObjectOne" and
// assigns it to the 'referenceObject' variable
// Now you need to get the component called 'ScriptOne' that's attached to the object.
referenceScript = referenceObject.GetComponent<PlayerController>();
// you call GetComponent <ComponentType>() on the referenceObject and it returns
// the component of the type that you specified between < > (if it has one).
// Now you can change the variable inside the ObjectOne, from the inside of ObjectTwo
referenceScript.integerToChange = 1;
// You can launch a method that's defined in 'ScriptOne' as well
*/


            }
        }
        catch (Exception e)
        {
            print("error");
        }


    }

}
