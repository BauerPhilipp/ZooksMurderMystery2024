using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioVolumeManager : MonoBehaviour
{

    public static AudioVolumeManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Value between 0...1;
    /// </summary>
    public float AudioVolume 
    {
        get {
            return audioSource.volume;
        }
        set {
            if(value < 0.0f)
            {
                audioSource.volume = 0;
            }
            else if(value > 1)
            {
                audioSource.volume = 1;
            }
            else
            {
                audioSource.volume = value;
            }


        }
    }

}
