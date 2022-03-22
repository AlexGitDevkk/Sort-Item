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

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _movePlay;
    [SerializeField] private AudioClip _winPlay;
    [SerializeField] private AudioClip _losePlay;

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    private IEnumerator StartCor()
    {
        yield return new WaitForFixedUpdate();
        audioImage.sprite = AudioEnabled ? on : off;
        yield break;
    }

    public void MovePlay()
    {
        if (!AudioEnabled)
            return;
        SoundPlay(_movePlay);
    }

    public void EndLevelPlay(bool _)
    {
        if (!AudioEnabled)
            return;
        if(_)
            SoundPlay(_winPlay);
        else
            SoundPlay(_losePlay);
    }

    private void SoundPlay(AudioClip _)
    {
        _audioSource.clip = _;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.Play();
    }

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
