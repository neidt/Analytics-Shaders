using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

public class LoadSaveLevelData : MonoBehaviour
{
    private LevelData levelData = new LevelData();


    public GameObject obstacleTemplate;
    public GameObject pickupTemplate;
    public GameObject objectTemplate;
    public Button saveLevel;
    public InputField txtLevelName;
    public Toggle toggleMovePlace;
    //public Toggle togglePickupObstacle;

    public CameraMove cam;

    private EditingController editingController;

    // Use this for initialization
    void Start()
    {
        editingController = GameObject.Find("GameController").GetComponent<EditingController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveLevel)
        {
            SaveLevel(txtLevelName.text);
        }

        if (toggleMovePlace.isOn)
        {
            cam.enabled = true;
            //spawn glowy object
            if (Input.GetKeyDown(KeyCode.G))
            {
                GameObject.Instantiate(pickupTemplate, Vector3.zero, Quaternion.identity);
            }

            //make not a pickup
            if (Input.GetKeyDown(KeyCode.H))
            {
                GameObject.Instantiate(objectTemplate, Vector3.zero, Quaternion.identity);
            }
        }
    }

    private void SaveLevel(string levelName)
    {
        LevelData level = new LevelData();
        //levelData.movingSpheres = new List<MovingSphereData>();

        level.SetLevelName(editingController.levelName);

        //ground
        Transform groundPlane = GameObject.FindGameObjectWithTag("Ground").GetComponent<Transform>();
        level.groundPlane.width = groundPlane.localScale.x;
        level.groundPlane.height = groundPlane.localScale.y;

        ////player fov
        //level.fov = gameController.playerFov;

        ////detection dist
        //level.playerDetectionDist = gameController.playerDetectDistance;

        //obstacles
        foreach (GameObject pickup in GameObject.FindGameObjectsWithTag("Pickup"))
        {
            PickupData newPickup = new PickupData();
            newPickup.position = pickup.transform.position;
            newPickup.rotation = pickup.transform.localRotation;

            level.pickupList.Add(newPickup);
        }


        //other stuffs spheres
        foreach (GameObject objectBoulder in GameObject.FindGameObjectsWithTag("Boulder"))
        {
            NonPickupData currentObject = new NonPickupData();
            currentObject.position = objectBoulder.transform.position;
            currentObject.rotation = objectBoulder.transform.localRotation;

            levelData.objectList.Add(currentObject);
        }
        level.SaveToFile(levelName + ".lvl");
    }//end savelevel

}

[Serializable]
public class Ground
{
    public float width, height, xScale, yScale;

    public Ground()
    {
        width = 1;
        height = 1;
        xScale = 1;
        yScale = 1;
    }
}

//contains the data from the pickups we want to save
[Serializable]
public class PickupData
{
    public Color glowColor;
    public Vector3 position;
    public Transform transform;
    public Quaternion rotation;
}

[Serializable]
public class NonPickupData
{
    public Vector3 position;
    public Transform transform;
    public Quaternion rotation;
}

[Serializable]
public class LevelData
{
    public string levelName;
    public Ground groundPlane = new Ground();
    public List<PickupData> pickupList = new List<PickupData>();
    public List<NonPickupData> objectList = new List<NonPickupData>();

    #region get/set methods for the level data itself
    public string GetLevelName()
    {
        return levelName;
    }
    public void SetLevelName(string levelName)
    {
        this.levelName = levelName;
    }
    #endregion

    #region save and load levelData functions
    public static LevelData LoadFromFile(string fileName)
    {
        return JsonUtility.FromJson<LevelData>(System.IO.File.ReadAllText(fileName));
    }//end loadfromfile

    public void SaveToFile(string fileName)
    {
        System.IO.File.WriteAllText(fileName, JsonUtility.ToJson(this, true));
    }//end savetofile
    #endregion
}
