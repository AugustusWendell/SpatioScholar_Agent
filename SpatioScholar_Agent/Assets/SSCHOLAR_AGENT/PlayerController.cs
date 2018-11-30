using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;
    public bool vector_render;
    public bool attributes_render;
    public bool debug_rays;
    public bool attributes_visible;
    public float sky_exposure;
    public float temp_sky_exposure;
    private float hit;
    public bool intervisibility;
    public GameObject home;
    public GameObject target;
    public Mesh mesh;
    public Bounds bounds;
    public Vector3 bound_center;
    public Vector3 bound_extents;
    public float x_div;
    public float y_div;
    public float z_div;
    public float x_start;
    public float y_start;
    public float z_start;

    // Use this for initialization
    void Start () {
        //print("PlayerController initialization");
        temp_sky_exposure = 0;
        debug_rays = true;

        target = GameObject.Find("Intervisibility Target");
        //Mesh mesh = target.GetComponent<MeshFilter>().mesh;
        Renderer mesh = target.GetComponent<Renderer>();
        bounds = mesh.bounds;
        bound_center = bounds.center;
        Debug.Log(bound_center);
        bound_extents = bounds.extents;
        Debug.Log(bound_extents);
    }
	
	// Update is called once per frame
	void Update () {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //call the intervisibility Raycast method
        //Intervisibility_Raycast();
        Intervisibility_Bounding_Raycast(8);

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
        print("Toggle Intervisibility");
        intervisibility = false;
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
       //print("Intervisibility Bounding Raycast Called");
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
                    Debug.Log(test_target);
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
                        
                        if (hit.collider.gameObject.name == "Intervisibility Target")
                        {
                            //print("Ray Hit!");
                            Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.red);
                            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                        }

                        //Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.white);
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
                Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.red);
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }

            Debug.DrawRay(NewRayCastLocation, RayDirection * 50, Color.white);
        }
        else
        {
            print("Ray not Hit!");
        }
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
}
