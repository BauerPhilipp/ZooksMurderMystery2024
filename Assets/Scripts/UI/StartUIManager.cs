using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class StartUIManager : MonoBehaviour
{
    VisualElement root;
    VisualElement startButton;
    Label startButtonLabel;
    VisualElement optionsButton;
    Label optionsButtonLabel;
    VisualElement creditsButton;
    Label creditsButtonLabel;

    VisualElement settingsContainer;

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
        SceneManager.LoadScene(1);
    }

    private void OptionsButtonClicked(ClickEvent e)
    {
        settingsContainer.visible = !settingsContainer.visible;
    }



}
