using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BindCameraToPlayer : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Transform mainCamera = Camera.main.transform;
        mainCamera.parent = transform.Find("EyeMount");
        mainCamera.position = mainCamera.parent.position;
        mainCamera.rotation = mainCamera.parent.rotation;
        mainCamera.localScale = Vector3.one;

    }

}
