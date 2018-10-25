using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float runMultiplier;
    private bool isRunning = false;
    private float MAXSPEED;
    private float BASESPEED;
    public float rotateFactor = 500.0f;
    public float pitchFactor = 500.0f;
    public int pickupCount = 0;

    private Transform eyeMount;
    private CharacterController characterController;

    public LayerMask raycastLayers;
    public LayerMask PickupOnly;
    public bool isHittingObj = false;

    public float rayDistance = 3.0f;


    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();

        eyeMount = transform.Find("EyeMount");

        speed = 10;
        BASESPEED = speed;
        MAXSPEED = speed * runMultiplier;
    }

    private void Update()
    {
        #region movement
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed = BASESPEED;
        }
        if (isRunning)
        {
            speed *= runMultiplier;
            if (speed > MAXSPEED) speed = MAXSPEED;
        }

        //movement
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection += transform.forward;
        if (Input.GetKey(KeyCode.A)) moveDirection += -transform.right;
        if (Input.GetKey(KeyCode.S)) moveDirection += -transform.forward;
        if (Input.GetKey(KeyCode.D)) moveDirection += transform.right;

        transform.Rotate(Vector3.up, rotateFactor * (Input.GetAxis("Mouse X") * Time.deltaTime));
        if (eyeMount != null)
        {
            eyeMount.Rotate(Vector3.right, -rotateFactor * (Input.GetAxis("Mouse Y") * Time.deltaTime));
            //boomBoomStick.Rotate(Vector3.forward, rotateFactor * (Input.GetAxis("Mouse Y") * Time.deltaTime));
        }
        //transform.Translate(moveDirection * speed * Time.deltaTime);
        characterController.SimpleMove(moveDirection.normalized * speed);
        #endregion

        CheckForObject();
    }

    public void CollectObject(GameObject obj)
    {
        obj.SetActive(false);
        pickupCount++;
    }

    public void CheckForObject()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, rayDistance, raycastLayers.value))
        {
            print("raycast hit" + hitInfo.transform.name + " at" + hitInfo.point);
            isHittingObj = true;

            if (hitInfo.transform.tag == "Pickup" && Input.GetKeyDown(KeyCode.Space))
            {
                GameObject hitInfoObj = hitInfo.transform.gameObject;
                if (hitInfoObj.GetComponent<GlowObject>().isGlowing == true)
                {
                    hitInfoObj.GetComponent<GlowObject>().GlowColor = Color.blue;
                }

                CollectObject(hitInfoObj);
            }
        }
    }
}
