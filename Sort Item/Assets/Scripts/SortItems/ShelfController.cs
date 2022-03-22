using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    [Header("Count Items in:")]
    public int Rows = 0;
    public int Column = 0;
    [Space]
    [SerializeField] private GameObject _pointPrefab;
    private List<Transform> _row = new List<Transform>();
    [HideInInspector] public List<List<Transform>> Points = new List<List<Transform>>();
    [Header("Win Particle: ")]
    [SerializeField] private ParticleSystem _winParticle;

    #region Singleton

    public static ShelfController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #endregion

    private void Start()
    {
        Vector3 position = Vector3.zero;
        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                position = new Vector3(j * 1, i * 2, 0.75f);
                var p = Instantiate(_pointPrefab, position, Quaternion.identity, transform);
                _row.Add(p.transform);

            }
            Points.Add(_row.GetRange(_row.Count - Rows, _row.Count));
            _row.Clear();
        }
    }

    public void WinPartPlay()
    {
        _winParticle.Play();
    }

}
