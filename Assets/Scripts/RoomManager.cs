using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{

    Scene activeScene;
    private int sceneCount;

    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        sceneCount = SceneManager.sceneCountInBuildSettings;
    }

    public void LoadNextRoom()
    {
        if(activeScene.buildIndex == sceneCount -1)
        {
            Debug.Log("Last Room! Try to go back!");
            return; 
        }
        SceneManager.LoadScene(activeScene.buildIndex + 1);
    }

    public void LoadPreviousRoom()
    {
        if (activeScene.buildIndex == 1)
        {
            Debug.Log("First Room! Try to go other direction!");
            return;
        }
        SceneManager.LoadScene(activeScene.buildIndex - 1);
    }

}
