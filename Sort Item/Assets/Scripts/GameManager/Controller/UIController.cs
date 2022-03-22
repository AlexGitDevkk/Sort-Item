using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public int Money = 0;

    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject loseUI;
    [SerializeField] private GameObject tutorialUI;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI levelIndexText;
    [SerializeField] private TextMeshProUGUI gradeText;

    #region Singleton

    public static UIController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    private IEnumerator StartCor()
    {
        yield return new WaitForFixedUpdate();
        LevelController.Instance.LevelIndexReload(levelIndexText);
        MoneyUpdate();
        yield break;
    }

    public void StopTutorial()
    {
        tutorialUI.SetActive(false);
    }

    public void LevelEnd(bool _flag)
    {
        if (_flag)
        {
            loseUI.SetActive(false);
            winUI.SetActive(true);
        }
        else
        {
            winUI.SetActive(false);
            loseUI.SetActive(true);
        }
    }

    public void MoneyUpdate()
    {
        moneyText.text = "" + Money;
    }

    public void GradeChangeCor(int maxGrade)
    {
        IEnumerator GradeChange()
        {
            for (float t = 0; t < 1; t += Time.deltaTime / 2f)
            {
                gradeText.text = "" + (int)Mathf.Lerp(0, maxGrade, t);
                yield return null;
            }
            gradeText.text = "" + maxGrade;
        }
        StartCoroutine(GradeChange());
    }
}

// UIController.Instance.StopTutorial();                 - to stop Tutorial Panel
// UIController.Instance.LevelEnd(bool _flag);           - to show Win/Lose Panel
// UIController.Instance.MoneyUpdate();                  - to show current count of money
// UIController.Instance.GradeChangeCor(int maxGrade);   - to smoothly show grade count
// Yours ever 3R
