using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameUIManager : MonoBehaviour
{
    VisualElement root;
    VisualElement startButton;
    Label startButtonLabel;
    VisualElement optionsButton;
    Label optionsButtonLabel;
    VisualElement creditsButton;
    Label creditsButtonLabel;

    VisualElement settingsContainer;

    public event Action OnOptionsPressed;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q("StartButton");
        startButtonLabel = root.Q<Label>("StartButtonLabel");

        optionsButton = root.Q("OptionsButton");
        optionsButtonLabel = root.Q<Label>("OptionsButtonLabel");

        creditsButton = root.Q("CreditsButton");
        creditsButtonLabel = root.Q<Label>("CreditsButtonLabel");

        settingsContainer = root.Q("SettingsContainer");
        settingsContainer.visible = false;


        startButton.RegisterCallback<ClickEvent>(StartButtonClicked);
        optionsButton.RegisterCallback<ClickEvent>(OptionsButtonClicked);    

    }

    private void StartButtonClicked(ClickEvent e)
    {

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OptionsButtonClicked(ClickEvent e)
    {
        settingsContainer.visible = !settingsContainer.visible;
        OnOptionsPressed?.Invoke();
    }



}
