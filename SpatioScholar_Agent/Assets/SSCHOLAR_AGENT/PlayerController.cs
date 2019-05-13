using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    public SScholar_Agent_Controller controller;
    public Camera cam;
    public NavMeshAgent agent;
    public bool vector_render = false;
    public bool attributes_render = false;
    public bool debug_rays = false;
    public bool attributes_visible = false;
    public float sky_exposure;
    public float temp_sky_exposure;
    private float hit;
    public bool intervisibility = false;
    public GameObject home;
    public GameObject target;
    public Mesh mesh;

    public Renderer rend2;
    public Bounds bounds;
    public Vector3 bound_center;
    public Vector3 bound_extents;
    public float x_div;
    public float y_div;
    public float z_div;
    public float x_start;
    public float y_start;
    public float z_start;
    public int samplesubdiv = 3;
    public Color AgentColor;

    public bool dropmarker = false;

    public string Type;
    public string Block;
    public string Ward;
    public string ID;
    public string Sex;
    public string State;
    public int Total_Number;
    public Color Agent_Color;
    public string HomeObjectName = "Home_ObjectName";
    public GameObject HomeObject;
    public int clock_hour = 0;

    //for color change to code agents
    Material m_Material;

    /*
    public Dictionary<string, string> Itinerary = new Dictionary<string, string>()
    {
        { "110", "Target 1" },
        { "320", "Target 2" },
        { "530", "Target 1" }
    };
    */
    public Dictionary<string, string> Itinerary = new Dictionary<string, string>();

    // Use this for initialization
    void Start () {
        //print("PlayerController initialization");
        temp_sky_exposure = 0;
        debug_rays = true;
        samplesubdiv = 6;


        target = GameObject.Find("Intervisibility Target");
        if (target != null)
        {
            Debug.Log("intervisibility target(s) found!");
            //Renderer rend = target.GetComponent<Renderer>();
            //Renderer rend2 = target.GetComponent<Renderer>();
            //Collider m_Collider = target.GetComponent<Collider>();
            Renderer rend = target.GetComponent<Renderer>();
            bounds = rend.bounds;
            bound_center = bounds.center;
            //Debug.Log(bound_center);
            bound_extents = bounds.extents;
            //Debug.Log(bound_extents);
        }
        else
        {
            Debug.Log("no intervisibility target(s) found!");
        }


        /*
        //set color
        Component[] renderers;

        renderers = GetComponentsInChildren(typeof(Renderer));

        if (renderers != null)
        {
            //Debug.Log("found agent sub object renderer objects, going to change color now")
            foreach(Renderer r in renderers)
            {
                r.material.color = Color.yellow;
            }
        }
        else
        {
            //do nothing
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        if(intervisibility == true) {
            //call the intervisibility Raycast method
            Debug.Log("intervisibility toggle = true, calling the bounding raycast method");
            Intervisibility_Bounding_Raycast(samplesubdiv);
        }

        if(dropmarker == true)
        {
            Debug.Log("Dropping visibility marker");
            //drop object to mark successful hit
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.AddComponent<Rigidbody>();
            cube.transform.position = gameObject.transform.position;
            Renderer rend = cube.GetComponent<Renderer>();
            rend.material.shader = Shader.Find("_Color");
            rend.material.SetColor("_Color", Color.green);
        }

        /*  put into a separate method for calling specific sky exposure raycasts
        if (debug_rays = true) {
            
        //Test RayTrace Code
        int raycount = 20;
        int raynumber = raycount;
        //Vector3 RayDirection = new Vector3(1, 0, 1);
        //Code below randomizes the initial ray orientation to create a monte carlo (?) random sampling
        Vector3 RayDirection = new Vector3(Random.Range(0f,1f), Random.Range(0f, .25f), 0);
        sky_exposure = 0;
        Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 0.0f)));
            for (int i = 0; i < raynumber; i++)
            {
                //visualize the ray being cast in the interface
                //Debug.DrawRay(NewRayCastLocation, RayDirection * 100, Color.green);

                //if (Physics.Raycast(transform.position, RayDirection, 300))
                if (Physics.Raycast(NewRayCastLocation, RayDirection, 300))
                {
                    print("Ray Hit!");
                    temp_sky_exposure = temp_sky_exposure + (1f / raycount);
                    Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.white);
                    print("sky_exposure variable = " + temp_sky_exposure);
                }
                else
                {
                    print("Ray not Hit!");
                }
                //update sky_exposure
                sky_exposure = temp_sky_exposure;
                //rotate Ray Direction Vector3
                RayDirection = Quaternion.Euler(0, (360 / raynumber), 0) * RayDirection;
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
        if (attributes_render == false)
        {
            if (attributes_visible == true)
            {
                attributes_visible = false;
            }
        }

        //Test for Attribute Render Flag to turn on Canvas
        if (attributes_render == true)
        {
            if (attributes_visible == false){
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

    public void ToggleIntervisibility()
    {
        //debug
        print("Agent Instance Toggle Intervisibility method called");
        if (intervisibility == false)
        {
            Debug.Log("setting intervisibility == true");
            intervisibility = true;
        }
        else
        {
            /*
            if (intervisibility == true)
            {
                Debug.Log("setting intervisibility == false");
                intervisibility = false;
            }
            else
            {

            }
            */
        }
    }

    public void ToggleDebugRays()
    {
        if (debug_rays == false)
        {
            debug_rays = true;
        }
        else
        {
            debug_rays = false;
        }
    }

    public void Intervisibility_Bounding_Raycast(int x)
    {
        print("Intervisibility Bounding Raycast Called");
        dropmarker = false;
        x_div = (bound_extents.x * 2) / x;
        y_div = (bound_extents.y * 2) / x;
        z_div = (bound_extents.z * 2) / x;
        x_start = bound_center.x - bound_extents.x;
        y_start = bound_center.y - bound_extents.y;
        z_start = bound_center.z - bound_extents.z;

        for (int i = 0; i < (x); i++)
        {
            //x
            y_start = bound_center.y - bound_extents.y;
            for (int j = 0; j < (x); j++)
            {
                //y
                z_start = bound_center.z - bound_extents.z;
                for (int k = 0; k < (x); k++)
                {
                    //z
                    //Vector3 RayDirection = target.transform.position - transform.position;
                    Vector3 test_target = new Vector3(x_start, y_start, z_start);
                    //Debug.Log(test_target);
                    Vector3 RayDirection = test_target - transform.position;
                    Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 0.0f)));

                    // Bit shift the index of the layer (8) to get a bit mask
                    int layerMask = 1 << 8;
                    // This would cast rays only against colliders in layer 8.
                    // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
                    layerMask = ~layerMask;
                    RaycastHit hit;

                    //if (Physics.Raycast(NewRayCastLocation, RayDirection, 300) && )
                    if (Physics.Raycast(NewRayCastLocation, RayDirection, out hit, Mathf.Infinity, layerMask))
                    {
                        //moved this earlier in an effort to have the red rays overwrite the white
                        Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.gray);
                        if (hit.collider.gameObject.name == "Intervisibility Target")
                        {
                            print("Ray Hit!");

                            dropmarker = true;
                            

                            if (debug_rays == true)
                            {
                                Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.red); 
                            }
                            //Fetch the Material from the Renderer of the GameObject
                            m_Material = hit.collider.GetComponent<Renderer>().material;
                            m_Material.color = Color.red;
                            //hit.collider.gameObject().material.color = Color.red;

                            //Debug.Log(hit.textureCoord);
                            //Vector2 pixelUV = hit.textureCoord;
                            //SScholar_Agent_Intervisibility_Target target = hit.collider.gameObject.GetComponent<SScholar_Agent_Intervisibility_Target>();
                            //target.DrawTexture(pixelUV.x, pixelUV.y);

                            //hit.collider.gameObject.GetComponent<SScholar_Agent_Intervisibility_Target>().
                        }
                    }
                    else
                    {
                        //print("Ray not Hit!");
                    }
                    //z
                    z_start = z_start + z_div;
                }
                //y
                y_start = y_start + y_div;
            }
            //x
            x_start = x_start + x_div;
        }
    }
    public void Find_Intervisibility_Object()
    {
        print("Intervisibility Raycast Called");

        GameObject target = GameObject.Find("Intervisibility Target");

        Vector3 RayDirection = target.transform.position - transform.position;
        Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 0.0f)));
    }
    public void Intervisibility_Raycast()
    {
        print("Intervisibility Raycast Called");

        GameObject target = GameObject.Find("Intervisibility Target");

        Vector3 RayDirection = target.transform.position - transform.position;
        Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 0.0f)));

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;
        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;

        //if (Physics.Raycast(NewRayCastLocation, RayDirection, 300) && )
        if (Physics.Raycast(NewRayCastLocation, RayDirection, out hit, Mathf.Infinity, layerMask))
        {
            print("Ray Hit!");
            if(hit.collider.gameObject.name == "Intervisibility Target")
            {
                if(controller.visibility_marker == true)
                {
                    //drop a marker
                    Vector3 AddMarkerLocation = gameObject.transform.position;
                    Quaternion AddMarkerRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
                    //GameObject marker = Instantiate(Visibility_Marker, AddMarkerLocation, AddMarkerRotation);
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.AddComponent<Rigidbody>();
                    cube.transform.position = gameObject.transform.position;


                }
                if (debug_rays == true)
                {
                    Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.red);
                }
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.white);
        }
        else
        {
            print("Ray not Hit!");
        }
    }
    public void RunTests()
    {
        //how to figure out which tests are active and run them? Through the JSON? Through the UI?
    }
    public int FindAgentsInView(int CullingDistance)
    {
        int return_value = 0;
        /*
         * For each agent in the simulation{
         * Send one raycast from this agent to the target agent, if the raycast is successful and length is lesser than the CullingDistance then increment the return value
         * }
         */
        return (return_value);
    }

    public void Sky_Exposure()
    {
        //Test RayTrace Code
        int raycount = 20;
        int raynumber = raycount;
        //Vector3 RayDirection = new Vector3(1, 0, 1);
        //Code below randomizes the initial ray orientation to create a monte carlo (?) random sampling
        Vector3 RayDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, .25f), 0);
        sky_exposure = 0;
        Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 0.0f)));
        for (int i = 0; i < raynumber; i++)
        {
            //visualize the ray being cast in the interface
            //Debug.DrawRay(NewRayCastLocation, RayDirection * 100, Color.green);

            //if (Physics.Raycast(transform.position, RayDirection, 300))
            if (Physics.Raycast(NewRayCastLocation, RayDirection, 300))
            {
                print("Ray Hit!");
                temp_sky_exposure = temp_sky_exposure + (1f / raycount);
                Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.white);
                print("sky_exposure variable = " + temp_sky_exposure);
            }
            else
            {
                print("Ray not Hit!");
            }
            //update sky_exposure
            sky_exposure = temp_sky_exposure;
            //rotate Ray Direction Vector3
            RayDirection = Quaternion.Euler(0, (360 / raynumber), 0) * RayDirection;
        }
    }


    /*
    //deprecated
    public void Sky_Exposure()
    {
        //run a test to see what percentage of sky is hit
        int raynumber = 10;
        int RaysHit = 0;
        Vector3 NewRayCastLocation = (transform.position + (new Vector3(0.0f, 2.0f, 2.0f)));
        for (int i = 0; i < raynumber; i++)
        {
            Debug.DrawRay(NewRayCastLocation, new Vector3(1, 1, 1) * 50, Color.white);
            RaycastHit objectHit;
            // Shoot raycast
            if (Physics.Raycast(NewRayCastLocation, new Vector3(1,1,1) , out objectHit, 50))
            {
                //Debug.DrawRay(transform.position, fwd * 50, Color.green);
                RaysHit++;
            }
        }

    }
    */
    public void SetHour(int a, string b)
    {
        Vector3 newtargetposition = new Vector3(0,0,0);
        NavMeshAgent tempNav = gameObject.GetComponent<NavMeshAgent>();
        //Debug.Log("Agent SetHour called with " + a + "  " + b);
        clock_hour = a;
        //if my itinerary has a place to go at this hour then execute a change in target
        if (Itinerary.ContainsKey(b))
        {
            string TT = Itinerary[b];
           // Debug.Log("Key found in Itinerary Dictionary   Value  = " + TT);
            if(Itinerary[b] == "Home")
            {
                //go home
                newtargetposition = gameObject.GetComponent<PlayerController>().HomeObject.transform.position;
                controller.clock.timescaler = 500;
            }
            else
            {
                GameObject TT1 = GameObject.Find(TT);
                //Debug.Log("Setting Target to    " + TT1);
                if (TT1.GetComponent<SScholar_Agent_Target_Logic>() != null)
                {
                    //query target for whatever type of location the target knows to deliver, for instance a random value within it.
                    newtargetposition = TT1.GetComponent<SScholar_Agent_Target_Logic>().GetTargetLocation();

                    controller.clock.timescaler = 500;
                }
                else
                {
                    newtargetposition = TT1.transform.position;
                    controller.clock.timescaler = 500;
                }

                //Debug.Log("new target Vector3 position = " + newtargetposition);
            }
            
            
            tempNav.SetDestination(newtargetposition);
            tempNav.speed = 2;
            //Debug.Log("Current NavMeshAgent destination Vector3 = " + tempNav.destination);
            
        }
    }

    public void SetColor(Color c)
    {
        Debug.Log("Agent SetColor method called");
        //set color
        Component[] renderers;

        renderers = GetComponentsInChildren(typeof(Renderer));

        if (renderers != null)
        {
            //Debug.Log("found agent sub object renderer objects, going to change color now")
            foreach (Renderer r in renderers)
            {
                r.material.color = c;
            }
        }
        else
        {
            //do nothing
        }
    }

    public void Init_Agent_From_JSON(AgentInit a)
    {
        Debug.Log("Instance of new Agent Init method called");
        if (a.Color == "Red")
        {
            Debug.Log("Agent color definition in JSON registers as Red");
            SetColor(Color.red);
        }
        else
        {
            if (a.Color == "Yellow")
            {
                Debug.Log("Agent color definition in JSON registers as Yellow");
                SetColor(Color.yellow);
            }
            else
            {

                if (a.Color == "White")
                {
                    Debug.Log("Agent color definition in JSON registers as White");
                    SetColor(Color.white);
                }
                else
                {
                    Debug.Log("Agent color definition in JSON does NOT register as Red");
                    SetColor(Color.blue);
                }
            }
        }
    }
}
