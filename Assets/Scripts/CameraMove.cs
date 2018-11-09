using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform eye;
    // Use this for initialization
    void Start()
    {
        eye = GameObject.Find("Eye").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += -transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += -transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += transform.right;
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.position += transform.up;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.position += -transform.up;
        }

        this.transform.Rotate(Vector3.up, 170f * (Input.GetAxis("Mouse X") * Time.deltaTime));
        eye.Rotate(Vector3.right, -170f * (Input.GetAxis("Mouse Y") * Time.deltaTime));
    }
}
