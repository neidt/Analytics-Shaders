using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public Text txtLevelName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
       
	}

    public void btnCreateNewLevel()
    {
        //switch to blank level
        //UnityEngine.SceneManagement.SceneManager.UnloadScene("MainMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("EditingScene");
    }

    public void btnLoadLevel()
    {
        //load the data based on its coresponding input box value
        
        LevelData.LoadFromFile("Test1");
    }
}
