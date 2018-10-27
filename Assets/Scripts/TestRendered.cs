using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRendered : MonoBehaviour
{
    
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<Renderer>().IsVisibleFrom(Camera.main))
        {
            Debug.Log("Visible");
        }
        else
        {
            Debug.Log("Not visible");
        }
    }
}
