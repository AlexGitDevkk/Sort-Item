using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private bool isFirstScene = false;
    private int allLevels;
    private int lvlIndex;
    public int CountOfLoops;

    #region Singleton

    public static LevelController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {      
        if (PlayerPrefs.HasKey("Level") && isFirstScene)
        {
            int index = PlayerPrefs.GetInt("Level");
            PlayerPrefs.DeleteKey("Level");
            SceneManager.LoadScene(index);
        }
        allLevels = SceneManager.sceneCountInBuildSettings;
        lvlIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LevelReload()
    {
        SceneManager.LoadScene(lvlIndex);
    }

    public void NextLevel()
    {
        lvlIndex++;
        if (lvlIndex >= allLevels)
        {
            lvlIndex = 0;
            CountOfLoops++;
            Debug.Log(CountOfLoops);
        }
        SLS.Instance.Save();
        SceneManager.LoadScene(lvlIndex);
    }

    public void LevelIndexReload(TextMeshProUGUI levelIndexText)
    {
        int numb = 1 + lvlIndex + allLevels * CountOfLoops;
        levelIndexText.text = "Level " + numb;
    }
}

// LevelController.Instance.LevelReload();                                       - to reload current scene
// LevelController.Instance.NextLevel();                                         - to go to a new scene
// LevelController.Instance.LevelIndexReload(TextMeshProUGUI levelIndexText);    - to show current scene index
// Yours ever 3R