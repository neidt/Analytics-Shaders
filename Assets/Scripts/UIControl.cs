using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public InputField txtLevelName;
    public EditingController editController;

    // Use this for initialization
    void Start()
    {
        editController = GameObject.FindGameObjectWithTag("GameController").GetComponent<EditingController>();
    }

    // Update is called once per frame
    void Update()
    {
        editController.levelName = txtLevelName.text;

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
