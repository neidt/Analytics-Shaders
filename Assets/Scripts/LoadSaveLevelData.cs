using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

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

    private SimpleMover simpleMove;

    private CameraMove camMove;

    private EditingController editingController;

    // Use this for initialization
    void Start()
    {
        camMove = GameObject.FindGameObjectWithTag("CameraMount").GetComponent<CameraMove>() as CameraMove;
        editingController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EditingController>();

    }
    // Update is called once per frame
    void Update()
    {
        if (saveLevel)
        {
            SaveLevel(txtLevelName.text);
        }


        //cam.enabled = true;
        //spawn glowy object
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject.Instantiate(pickupTemplate, Vector3.zero, Quaternion.identity);

        }

        //make not a pickup
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Instantiate(objectTemplate, Vector3.zero, Quaternion.identity);
        }

        //make an obstacle
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject.Instantiate(obstacleTemplate, Vector3.zero, Quaternion.identity);
        }

        RaycastHit rayhit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayhit))
            {
                #region object movement, scaling, rotation
                if (rayhit.transform.tag == "Pickup" || rayhit.transform.tag == "Boulder")
                {
                    print("Hitting object");
                    simpleMove = rayhit.transform.gameObject.GetComponent<SimpleMover>() as SimpleMover;

                    camMove.enabled = false;
                    simpleMove.enabled = true;
                }
                if (rayhit.transform.tag == "Obstacle")
                {
                    simpleMove = rayhit.transform.gameObject.GetComponent<SimpleMover>() as SimpleMover;

                    camMove.enabled = false;
                    simpleMove.enabled = true;
                }
                if (rayhit.transform.tag == "Ground" || rayhit.transform.tag == "Untagged")
                {
                    simpleMove.enabled = false;
                    camMove.enabled = true;
                }
                #endregion
            }
        }
    }


    public void SaveLevel(string levelName)
    {
        LevelData level = new LevelData();
        
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
        //other stuffs
        foreach (GameObject objectBoulder in GameObject.FindGameObjectsWithTag("Boulder"))
        {
            NonPickupData currentObject = new NonPickupData();
            currentObject.position = objectBoulder.transform.position;
            currentObject.rotation = objectBoulder.transform.localRotation;

            levelData.objectList.Add(currentObject);
        }
        level.SaveToFile(levelName + ".unity");
        
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
    private static string saveDirectory = Directory.GetCurrentDirectory() + "\\CustomLevel\\";

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
        string newPath = System.IO.Path.Combine(saveDirectory, fileName);

        return JsonUtility.FromJson<LevelData>(System.IO.File.ReadAllText(newPath));
    }//end loadfromfile

    public void SaveToFile(string fileName)
    {
        string newPath = System.IO.Path.Combine(saveDirectory, fileName);
        System.IO.File.WriteAllText(newPath, JsonUtility.ToJson(this, true));
    }//end savetofile

    #endregion
}

public class SaveManager
{
    private LevelData dataToSave;
    string savePath;

    public SaveManager()
    {
        this.savePath = Application.persistentDataPath + "/save.dat";
        this.dataToSave = new LevelData();
        this.loadDataFromDisk();
    }

    public void saveDataToDisk()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, dataToSave);
        file.Close();
    }

    public void loadDataFromDisk()
    {
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            this.dataToSave = (LevelData)bf.Deserialize(file);
            file.Close();
        }
    }
}

