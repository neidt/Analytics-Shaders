using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsDemo : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Dictionary<string, object> additionalData = new Dictionary<string, object>();
        additionalData.Add("sample_number", 17);

        AnalyticsEvent.GameStart(additionalData);

        AnalyticsEvent.Custom("custom_demo_event", additionalData);

        AnalyticsEvent.Custom("obj_found_level1_obj");


        //alternatively
        /*
        Dictionary<string, object> findData = new Dictionary<string, object>();
        findData.Add("level", "mylebelNAme");
        findData.Add("obj_id", 1);
        findData.Add("time", (int)Time.timeSinceLevelLoad);
        AnalyticsEvent.Custom("obj_found", findData);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
    /*
    public void ReportObjFound(int objID)
    {
        AnalyticsEvent.Custom("object found", new Dictionary<string, object> { { "object_id", objID }, { "time_elapsed", Time.timeSinceLevelLoad } });
    }*/
}

