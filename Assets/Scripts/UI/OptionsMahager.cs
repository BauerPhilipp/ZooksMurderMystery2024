using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptionsMahager : MonoBehaviour
{

    VisualElement root;
    VisualElement volumeButton;
    float mouseStartClickPosition;

    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        volumeButton = root.Q("VolumeButton");

        volumeButton.RegisterCallback<MouseDownEvent>(VolumeMouseDownEvent);
    }

    private void VolumeMouseDownEvent(MouseDownEvent e)
    {
        //mouseStartClickPosition = Input.mousePosition.x;
        //volumeButton.style.position = 
    }

}
