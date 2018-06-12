using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
    public bool vector_render;
    public bool attributes_render;
    public bool attributes_visible;

        // Use this for initialization
        void Start () {
        //debug
        print("PlayerController initialization");
	}
	
	// Update is called once per frame
	void Update () {

        //Test RayTrace Code
        int raynumber = 10;
        //Vector3 RayDirection = new Vector3(1, 0, 1);
        Vector3 RayDirection = new Vector3(Random.Range(0f,1f), 0, Random.Range(0f, 1f));
        for (int i = 0; i < raynumber; i++)
        {
            Debug.DrawRay(transform.position, RayDirection * 50, Color.green);
            //rotate Ray Direction Vector3
            RayDirection = Quaternion.Euler(0, (360/raynumber), 0) * RayDirection;
        }

        //listener to move to the raycast location
        /*
		 if (Input.GetMouseButtonDown(0))
        {
            print("PlayerController Button Down Registered");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Move the Agent
                agent.SetDestination(hit.point);
                print("PlayerController moving agent based on raycast");
            }

        }
        */


        //Test for Vector Flag to turn on vector line renderer
        if (vector_render == true) {

            //invoke the LineRenderer to render the direction vector for this agent
            LineRenderer lineRenderer = this.GetComponent<LineRenderer>();
            lineRenderer.SetVertexCount(2);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.forward * 3 + transform.position);
        }

        //make sure atributes visible is turned off
        if (attributes_render = false)
        {
            if (attributes_visible = true)
            {
                attributes_visible = false;
            }
        }

        //Test for Attribute Render Flag to turn on Canvas
        if (attributes_render = true)
        {
            if (attributes_visible = false){
                attributes_visible = true;
            }

            //make the canvas visible
            //Canvas myCanvas = this.GetComponent<LineRenderer>();

        }
	}

    public void ToggleVector()
    {
        //debug
        print("Setting Vector Render False");
        vector_render = false;
    }

    public void Sky_Exposure()
    {
        //run a test to see what percentage of sky is hit
        int raynumber = 10;
        int RaysHit = 0;
        for (int i = 0; i < raynumber; i++)
        {
            Debug.DrawRay(transform.position, new Vector3(1, 1, 1) * 50, Color.green);
            RaycastHit objectHit;
            // Shoot raycast
            if (Physics.Raycast(transform.position, new Vector3(1,1,1) , out objectHit, 50))
            {
                //Debug.DrawRay(transform.position, fwd * 50, Color.green);
                RaysHit++;
            }
        }

    }
}
