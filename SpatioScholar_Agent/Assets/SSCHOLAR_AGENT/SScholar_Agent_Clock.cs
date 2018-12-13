using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SScholar_Agent_Clock : MonoBehaviour {

    public int hour = 0;
    public int minute = 0;
    public string display_time;
    public int timebuffer = 0;
    public int timescaler = 0;
    public string itinerary_time;

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
        string display_hour;
        string display_minute;

        if(hour < 10)
        {
            display_hour = "0" + hour.ToString();
        }
        else
        {

            display_hour = hour.ToString();
        }
        if (minute < 10)
        {
            display_minute = "0" + minute.ToString();
        }
        else
        {
            display_minute = minute.ToString();

        }
        display_time = "Hour " + display_hour + "   Minute " + display_minute;
        return display_time;
    }

    public string return_itinerary_time()
    {
        string display_hour;
        string display_minute;
            display_hour = hour.ToString();
            display_minute = minute.ToString();
        itinerary_time = display_hour + display_minute;
        return itinerary_time;
    }
}
