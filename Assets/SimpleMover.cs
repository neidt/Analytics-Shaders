using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movement += transform.up;
            Vector3 newPos = transform.position + movement;
            newPos.x = Mathf.Clamp(newPos.x, 0, 3);
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.E))
        {
            movement += -transform.up;
            Vector3 newPos = transform.position + movement;
            newPos.x = Mathf.Clamp(newPos.x, 0, 3);
            transform.position = newPos;
        }
    }
}
