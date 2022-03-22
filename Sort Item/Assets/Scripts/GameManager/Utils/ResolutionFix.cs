using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ResolutionFix))]
public class ResolutionFixEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ResolutionFix myScript = (ResolutionFix)target;
        if (GUILayout.Button("SetUp Envirenment"))
        {
            myScript.SetUpResolution();
        }
    }
}
#endif

public class ResolutionFix : MonoBehaviour
{
    private float _defaultRatio1 = 2.1642f;
    private float _defaultRatio2 = 1.3340f;

    [SerializeField] private float _defaultfocalL1 = 35f;      //IphoneLarge (1242x2688) perfect FOV
    [SerializeField] private float _defaultfocalL2 = 50f;      //Ipad (2048x2732) perfect FOV
    [SerializeField] private Camera _camera;

    private void Start()
    {
        SetUpResolution();
    }

    public void SetUpResolution()
    {
        var currRatio = (float)Screen.height / (float)Screen.width;
        var t = (currRatio - _defaultRatio2) / (_defaultRatio1 - _defaultRatio2);
        // print("t: " + t + "; lerp: " + Mathf.LerpUnclamped(_defaultfocalL2, _defaultfocalL1, t));
        _camera.fieldOfView = Mathf.LerpUnclamped(_defaultfocalL2, _defaultfocalL1, t);
    }
}

// You need to find the perfect FOV for IphoneLarge(1242x2688) and Ipad(2048x2732)
// Write these values ​​int _defaultfocalL1 and _defaultfocalL2
// Yours ever 3R