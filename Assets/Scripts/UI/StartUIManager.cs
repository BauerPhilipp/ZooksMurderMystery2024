using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class StartUIManager : MonoBehaviour
{
    PlayerControlls playerControlls;

    VisualElement root;
    VisualElement startButton;
    Label startButtonLabel;
    VisualElement optionsButton;
    Label optionsButtonLabel;
    VisualElement creditsButton;
    Label creditsButtonLabel;
    VisualElement settingsButton;
    VisualElement notesButton;
    VisualElement notesContainer;
    bool isNotesActive = false;
    TextField notesTextField;
    Button resetButton;
    string resetNotesString;


    VisualElement settingsContainer;
    VisualElement buttonContainer;

    private int maxTextLength = 55;  //max line length in notes field

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

        settingsButton = root.Q("SettingsButton");
        buttonContainer = root.Q("ButtonContainer");

        notesButton = root.Q("NotesButton");
        notesButton.RegisterCallback<ClickEvent>(NotesButtonClicked);
        notesContainer = root.Q("NotesContainer");
        notesTextField = root.Q<TextField>("NotesTextField");
        notesTextField.RegisterCallback<InputEvent>(NotesTextFieldInput);

        resetButton = root.Q<Button>("ResetButton");
        resetButton.RegisterCallback<ClickEvent>(ResetNotesButtonClicked);


        settingsButton.RegisterCallback<ClickEvent>(SettingsButtonPressed);
        startButton.RegisterCallback<ClickEvent>(StartButtonClicked);
        optionsButton.RegisterCallback<ClickEvent>(OptionsButtonClicked);


        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            buttonContainer.visible = true;
        }
        else
        {
            buttonContainer.visible = false;
        }


    }

    private void Start()
    {
        playerControlls = FindAnyObjectByType<PlayerController>().PlayerControlls;
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

    private void SettingsButtonPressed(ClickEvent e)
    {
        buttonContainer.visible = !buttonContainer.visible;
        settingsContainer.visible = false;

        //set NotesContainer Invisible
        if (isNotesActive)
        {
            isNotesActive = !isNotesActive;
            notesContainer.visible = isNotesActive;
        }
    }

    private void NotesTextFieldInput(InputEvent e)
    {
    }

    private void NotesButtonClicked(ClickEvent e)
    {
        isNotesActive = !isNotesActive;
        notesContainer.visible = isNotesActive;
        resetNotesString = notesTextField.value;

        //set Options/ButtonContainer Invisible
        if(isNotesActive)
        {
            buttonContainer.visible = false;
            settingsContainer.visible = false;
        }

        if (isNotesActive)
        {
            playerControlls.Disable();
        }
        else
        {
            playerControlls.Enable();
        }
    }
    private void ResetNotesButtonClicked(ClickEvent e)
    {
        notesTextField.value = resetNotesString;
    }



}
