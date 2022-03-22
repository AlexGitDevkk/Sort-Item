using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(EnvironmentSetting))]
public class EnvironmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnvironmentSetting myScript = (EnvironmentSetting)target;
        if (GUILayout.Button("SetUp Envirenment"))
        {
            myScript.SetUpEnvirenment();
        }
    }
}
#endif

public class EnvironmentSetting : MonoBehaviour
{
    [SerializeField] Material skyBox;

    [SerializeField] private bool isFog = true;
    [SerializeField] private Color fogColor = Color.white;
    [SerializeField] private float fogDensity = 0.0045f;

    private void Start()
    {
        SetUpEnvirenment();
    }

    public void SetUpEnvirenment()
    {
        RenderSettings.skybox = skyBox;
        RenderSettings.fog = isFog;
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;
    }
}

//You need to change the parameters from the inspector
// Yours ever 3R