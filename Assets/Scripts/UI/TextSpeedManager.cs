using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TextSpeedManager : MonoBehaviour
{

    VisualElement root;
    VisualElement textSpeedDragger;
    VisualElement textSpeedTracker;
    Label textSpeedLabel;


    float textSpeed = 0.5f;
    bool isTextSpeedPressed = false;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        textSpeedLabel = root.Q<Label>("TextSpeedLabel");

        textSpeedDragger = root.Q("TextSpeedDragger");

        textSpeedTracker = root.Q("TextSpeedTracker");
        textSpeedTracker.RegisterCallback<MouseDownEvent>(SliderTrackerMouseDown);
        textSpeedTracker.RegisterCallback<MouseMoveEvent>(SliderTrackerMouseMove);
        textSpeedTracker.RegisterCallback<MouseUpEvent>(SliderTrackerMouseUp);
        textSpeedTracker.RegisterCallback<MouseLeaveEvent>(SliderTrackerMouseLeave);

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SaveSettings();
        }
        else
        {
            LoadSettings();
        }

    }

    private void Start()
    {
        GetComponent<StartUIManager>().OnOptionsPressed += SetDraggerPosition;
    }

    private void SliderTrackerMouseDown(MouseDownEvent e)
    {
        isTextSpeedPressed = true;
        textSpeedDragger.style.left = e.localMousePosition.x - (textSpeedDragger.resolvedStyle.width / 2);
    }

    private void SliderTrackerMouseMove(MouseMoveEvent e)
    {
        if(!isTextSpeedPressed) { return; }
        textSpeedDragger.style.left = e.localMousePosition.x - (textSpeedDragger.resolvedStyle.width / 2);

        if (e.localMousePosition.x < 1)
        {
            textSpeedDragger.style.left = 1;
        }
        if(e.localMousePosition.x > textSpeedTracker.resolvedStyle.width - textSpeedDragger.resolvedStyle.width / 2)
        {
            textSpeedDragger.style.left = textSpeedTracker.resolvedStyle.width - (textSpeedDragger.resolvedStyle.width / 2);
        }

        textSpeed = MapSliderToTextSpeedValue();
        SetTextSpeedLabel();
    }

    private void SliderTrackerMouseUp(MouseUpEvent e)
    {
        textSpeed = MapSliderToTextSpeedValue();
        SetTextSpeedLabel();
        isTextSpeedPressed = false;
        SaveSettings();
    }

    private void SliderTrackerMouseLeave(MouseLeaveEvent e)
    {
        textSpeed = MapSliderToTextSpeedValue();
        SetTextSpeedLabel();
        isTextSpeedPressed = false;
        SaveSettings();

    }

    private float MapSliderToTextSpeedValue()
    {
        float normalizedValue = textSpeedDragger.resolvedStyle.left / textSpeedTracker.resolvedStyle.width;
        Debug.Log("normValueTextSlider: " + normalizedValue);
        return normalizedValue;
    }

    private void SetDraggerPosition()
    {
        float newDraggerValue = textSpeedTracker.resolvedStyle.width * textSpeed - textSpeedDragger.resolvedStyle.width / 2;
        Debug.Log(newDraggerValue);
        textSpeedDragger.style.left = newDraggerValue;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("TextSpeed", textSpeed);
    }

    private void LoadSettings()
    {
        textSpeed = PlayerPrefs.GetFloat("TextSpeed");
    }


    private void SetTextSpeedLabel()
    {
        textSpeedLabel.text = "Textspeed: " + (Mathf.RoundToInt(textSpeed * 100));
    }


}
