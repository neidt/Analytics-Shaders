using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMover : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        Vector3 xVec = new Vector3(1f, 0f, 0f);
        Vector3 yVec = new Vector3(0f, 1f, 0f);
        Vector3 zVec = new Vector3(0f, 0f, 1f);

        #region movement
        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += -transform.forward * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += -transform.right * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movement += transform.up * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            newPos.y = Mathf.Clamp(newPos.y, 0f, 3f);
            transform.position = newPos;
        }
        if (Input.GetKey(KeyCode.E))
        {
            movement += -transform.up * Time.deltaTime * 3;
            Vector3 newPos = transform.position + movement;
            newPos.y = Mathf.Clamp(newPos.y, 0f, 3f);
            transform.position = newPos;
        }
        #endregion

        #region rotation
        //x rotation
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(xVec);
        }
        if (Input.GetKey(KeyCode.F))
        {
            transform.Rotate(-xVec);
        }
        //yrotation
        if (Input.GetKey(KeyCode.T))
        {
            transform.Rotate(yVec);
        }
        if (Input.GetKey(KeyCode.G))
        {
            transform.Rotate(-yVec);
        }
        //zrotation
        if (Input.GetKey(KeyCode.Y))
        {
            transform.Rotate(zVec);
        }
        if (Input.GetKey(KeyCode.H))
        {
            transform.Rotate(-zVec);
        }
        #endregion

        #region scaleing
        if (Input.GetKey(KeyCode.U))
        {
            transform.localScale += transform.localScale * Time.deltaTime * 1f;
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.localScale += transform.localScale * Time.deltaTime * -1f;
        }
        #endregion
    }
}
