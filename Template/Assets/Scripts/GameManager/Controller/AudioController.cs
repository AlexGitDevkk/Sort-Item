using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    #region Singleton

    public static AudioController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    [SerializeField] private Image audioImage;
    [SerializeField] private Sprite on, off;
    public bool AudioEnabled = true;

    //[SerializeField] private AudioSource ExamplePlay;

    private void Start()
    {
        audioImage.sprite = AudioEnabled ? on : off;
    }

    //public void ExamplePlay()
    //{
    //    if (!AudioEnabled)
    //        return;
    //    ExamplePlay.pitch = Random.Range(0.8f, 1.2f);
    //    ExamplePlay.Play();
    //}

    public void Toggle()
    {
        AudioEnabled = !AudioEnabled;
        audioImage.sprite = AudioEnabled ? on : off;

        SLS.Instance.Save();

        TapTicController.Instance.Light();
    }

}

// AudioController.Instance.BGSoundPlay();       - to start BGSound
// Yours ever 3R
