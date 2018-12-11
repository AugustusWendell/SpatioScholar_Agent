using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SScholar_Agent_Clock : MonoBehaviour {

    public int hour = 0;
    public int minute = 0;
    public string display_time;
    public int timebuffer = 0;
    public int timescaler = 0;

	// Use this for initialization
	void Start () {
        Debug.Log("Clock initialized");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void increment_time()
    {
        increment_minute();
    }

    void increment_hour()
    {
        if(hour < 23)
        {
            hour++;
        }
        else
        {
            hour = 0;
        }

    }
    void increment_minute()
    {
        if (minute < 59)
        {
            
            if(timebuffer > timescaler)
            {
                minute++;
                timebuffer = 0;
            }
            else
            {
                timebuffer++;
            }
            

            //minute++;
        }
        else
        {
            increment_hour();
            minute = 0;
        }

    }
    public string return_time()
    {
        display_time = hour.ToString() + minute.ToString();
        return display_time;
    }
}
