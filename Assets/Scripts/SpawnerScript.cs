using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerScript : MonoBehaviour
{
    public GameObject pickupObj;
    public GameObject regularObj;
    public Transform ground;
    public int objectsCollected = 0;
    public int totPickupCount = 0;

    float XMin = -15f;
    float YMin = -15f;
    float XMax = 15;
    float YMax = 15;
    float rotX, rotY, rotZ;
    public int maxNumObjects = 10;
    int totalObjCount;

    // Use this for initialization
    void Start()
    {
        for (totalObjCount = 0; totalObjCount < maxNumObjects; totalObjCount++)
        {
            if (Random.Range(0,2) == 0)
            {
                //print("making pickup at " +MakeSpotVec().ToString());
                Instantiate(pickupObj, MakeSpotVec(), MakeSpotQuat());
                totPickupCount++;
            }
            else
            {
                //print("making regular object at: " + MakeSpotVec().ToString());
                Instantiate(regularObj, MakeSpotVec(), MakeSpotQuat());
            }
            //print("total objects is: " + totalObjCount.ToString());
        }
        
    }

    

    Vector3 MakeSpotVec()
    {
        float spotX = Random.Range(XMin, XMax);
        float spotZ = Random.Range(YMin, YMax);
        
        Vector3 spot = new Vector3(spotX, 1, spotZ);
        return spot;
    }

    Quaternion MakeSpotQuat()
    {
        rotX = Random.Range(-180f, 180f);
        rotY = Random.Range(-180f, 180f);
        rotZ = Random.Range(-180f, 180f);

        Quaternion spotRotation = new Quaternion(rotX, rotY, rotZ, 1);
        return spotRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(objectsCollected == totPickupCount)
        {
            GameObject.FindGameObjectWithTag("WinnerWinnerChickenDinner").GetComponent<Text>().text = "Winner Winner Chicken Dinner";
        }
    }
}
