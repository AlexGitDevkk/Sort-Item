using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapTicController : MonoBehaviour
{
    #region Singleton

    public static TapTicController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    [SerializeField] private Image tapticImage;
    [SerializeField] private Sprite on, off;
    public bool TapTicEnabled = true;

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    private IEnumerator StartCor()
    {
        yield return new WaitForFixedUpdate();
        tapticImage.sprite = TapTicEnabled ? on : off;
        yield break;
    }

    public void Warning()
    {
        Taptic.Warning();
    }

    public void Failure()
    {
        Taptic.Failure();
    }

    public void Success()
    {
        Taptic.Success();
    }

    public void Light()
    {
        Taptic.Light();
    }

    public void Medium()
    {
        Taptic.Medium();
    }

    public void Heavy()
    {
        Taptic.Default();
    }

    public void Toggle()
    {
        Taptic.tapticOn = !Taptic.tapticOn;
        TapTicEnabled = Taptic.tapticOn;
        tapticImage.sprite = TapTicEnabled ? on : off;

        Taptic.Selection();
        SLS.Instance.Save();

        TapTicController.Instance.Light();
    }

}

// TapTicController.Instance.Warning();       - to call TapTic Warning
// Yours ever 3R