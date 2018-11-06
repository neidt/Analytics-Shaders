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

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }
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
    public Quaternion rotation;
}

[Serializable]
public class NonPickupData
{
    public Vector3 position;
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
