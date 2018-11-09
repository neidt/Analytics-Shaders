using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public Text txtLevelName;
    private EditingController editController;

    // Use this for initialization
    void Start()
    {
        editController = GameObject.Find("GameController").GetComponent<EditingController>();
    }

    // Update is called once per frame
    void Update()
    {
        txtLevelName.text = editController.levelName;


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void btnCreateNewLevel()
    {
        //switch to blank level
        SceneManager.LoadScene("EditingScene");
    }

    public void btnLoadLevel(string levelToLoad)
    {
        //load the data based on its coresponding input box value

        LevelData.LoadFromFile(levelToLoad);
    }
}
