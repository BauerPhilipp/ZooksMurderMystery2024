using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OptionsManager : MonoBehaviour
{

    VisualElement root;
    VisualElement volumeDragger;
    VisualElement volumeTracker;
    Label volumeLabel;

    bool isVolumePressed = false;

    float mouseStartClickPosition;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        volumeLabel = root.Q<Label>("VolumeLabel");

        volumeDragger = root.Q("VolumeDragger");

        volumeTracker = root.Q("VolumeTracker");
        volumeTracker.RegisterCallback<MouseDownEvent>(SliderTrackerMouseDown);
        volumeTracker.RegisterCallback<MouseMoveEvent>(SliderTrackerMouseMove);
        volumeTracker.RegisterCallback<MouseUpEvent>(SliderTrackerMouseUp);
        volumeTracker.RegisterCallback<MouseLeaveEvent>(SliderTrackerMouseLeave);


    }

    private void Start()
    {
        GetComponent<StartUIManager>().OnOptionsPressed += SetDraggerPosition;

        if(!(SceneManager.GetActiveScene().buildIndex == 0))
        {
            AudioVolumeManager.Instance.AudioVolume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            AudioVolumeManager.Instance.AudioVolume = .5f;
        }
    }

    private void SliderTrackerMouseDown(MouseDownEvent e)
    {
        Debug.Log(e.localMousePosition);
        isVolumePressed = true;
        volumeDragger.style.left = e.localMousePosition.x - (volumeDragger.resolvedStyle.width / 2);
    }

    private void SliderTrackerMouseMove(MouseMoveEvent e)
    {
        if(!isVolumePressed) { return; }
        volumeDragger.style.left = e.localMousePosition.x - (volumeDragger.resolvedStyle.width / 2);

        if (e.localMousePosition.x < 1)
        {
            volumeDragger.style.left = 1;
        }
        if(e.localMousePosition.x > volumeTracker.resolvedStyle.width - volumeDragger.resolvedStyle.width / 2)
        {
            volumeDragger.style.left = volumeTracker.resolvedStyle.width - (volumeDragger.resolvedStyle.width / 2);
        }

        SetVolumeLabel();
        AudioVolumeManager.Instance.AudioVolume = MapSliderToVolumeValue();
    }

    private void SliderTrackerMouseUp(MouseUpEvent e)
    {
        isVolumePressed = false;
        SetVolumeLabel();
        AudioVolumeManager.Instance.AudioVolume = MapSliderToVolumeValue();
    }

    private void SliderTrackerMouseLeave(MouseLeaveEvent e)
    {
        AudioVolumeManager.Instance.AudioVolume = MapSliderToVolumeValue();
        SetVolumeLabel();
        isVolumePressed = false;
    }

    private float MapSliderToVolumeValue()
    {
        float normalizedValue = volumeDragger.resolvedStyle.left / volumeTracker.resolvedStyle.width;
        return normalizedValue;
    }

    private void SetDraggerPosition()
    {
        float newDraggerValue = volumeTracker.resolvedStyle.width * AudioVolumeManager.Instance.AudioVolume - volumeDragger.resolvedStyle.width / 2;
        Debug.Log(newDraggerValue);
        volumeDragger.style.left = newDraggerValue;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", AudioVolumeManager.Instance.AudioVolume);
    }

    private void SetVolumeLabel()
    {
        volumeLabel.text = "Volume: " + Mathf.RoundToInt(AudioVolumeManager.Instance.AudioVolume * 100);
    }


}
