using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Room : MonoBehaviour
{
    [SerializeField] private TextMeshPro preRoomText;
    private int sceneNumber;

    private void Awake()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;
        SetPreRoomText();
    }

    private void SetPreRoomText()
    {
        if(sceneNumber == 1) 
        {
            preRoomText.text = "";
        }
        else
        {
            preRoomText.text = "BACK TO\n ROOM " + (sceneNumber - 1);
        }
    }



}
