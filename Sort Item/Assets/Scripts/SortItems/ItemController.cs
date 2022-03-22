using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [HideInInspector] public bool IsInShelf = false;
    [HideInInspector] public int RowIndex;
    [HideInInspector] public int ColumnIndex;
    private Vector3 _startPos;
    private Vector3 _mOffset;
    private float _mZCoord;
    [SerializeField] private ParticleSystem _smallWin;
    [SerializeField] private float _scaleBoost = 1.1f;
    private PointController currentPoint;

    private void Start()
    {
        StartCoroutine(StartCor());
    }

    private IEnumerator StartCor()
    {
        yield return new WaitForFixedUpdate();
        StartCheckPositions();
        yield break;
    }

    private void OnMouseDown()
    {
        if (IsInShelf)
            return;

        if (currentPoint != null)
            currentPoint.isPlaced = false;

        _mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        _mOffset = transform.position - GetMouseWorldPos() - new Vector3(0, 0, 1f);
        _startPos = transform.position;
        StartCoroutine(ChangeScale(_scaleBoost));
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        if (IsInShelf)
            return;

        transform.position = GetMouseWorldPos() + _mOffset;
    }

    private void OnMouseUp()
    {
        if (IsInShelf)
            return;

        TakePlace();
    }

    private void StartCheckPositions()
    {
        for (int i = 0; i < ShelfController.Instance.Column; i++)
        {
            for (int j = 0; j < ShelfController.Instance.Rows; j++)
            {
                if (Vector3.Distance(transform.position, ShelfController.Instance.Points[i][j].position) <= 0.1f)
                {
                    currentPoint = ShelfController.Instance.Points[i][j].GetComponent<PointController>();
                    currentPoint.isPlaced = true;

                    if (ColumnIndex == j && RowIndex == i)
                    {  
                        IsInShelf = true;
                        currentPoint.isBusy = true;
                    }
                }    
            }
        }
    }

    private void TakePlace()
    {
        Vector3 itemPos = new Vector3(transform.position.x, transform.position.y, 0.75f);
        AudioController.Instance.MovePlay();

        for (int i = 0; i < ShelfController.Instance.Column; i++)
        {
            for (int j = 0; j < ShelfController.Instance.Rows; j++)
            {
                if (Vector3.Distance(itemPos, ShelfController.Instance.Points[i][j].position) <= 0.75f
                    && !ShelfController.Instance.Points[i][j].GetComponent<PointController>().isPlaced)
                {
                    currentPoint = ShelfController.Instance.Points[i][j].gameObject.GetComponent<PointController>();

                    if (ColumnIndex == j && RowIndex == i)
                    {
                        _smallWin.Play();
                        IsInShelf = true;
                        currentPoint.isBusy = true;
                    }

                    TapTicController.Instance.Light();

                    currentPoint.isPlaced = true;

                    StartCoroutine(MoveToStartPos(1f, ShelfController.Instance.Points[i][j].position));
                    CheckToWin();
                    return;
                }
            }
        }
        TapTicController.Instance.Heavy();
        StartCoroutine(MoveToStartPos(1f, _startPos));
    }

    private void CheckToWin()
    {
        int countBusy = 0;
        int countPlaced = 0;

        for (int i = 0; i < ShelfController.Instance.Rows; i++)
        {
            for (int j = 0; j < ShelfController.Instance.Column; j++)
            {
                if (ShelfController.Instance.Points[j][i].GetComponent<PointController>().isBusy)
                {
                    countBusy++;
                }
                if (ShelfController.Instance.Points[j][i].GetComponent<PointController>().isPlaced)
                {
                    countPlaced++;
                }
            }
        }

        if (countBusy == ShelfController.Instance.Column * ShelfController.Instance.Rows)
        {
            TapTicController.Instance.Success();
            ShelfController.Instance.WinPartPlay();
            AudioController.Instance.EndLevelPlay(true);
            UIController.Instance.LevelEnd(true);
        }
        else if (countPlaced == ShelfController.Instance.Column * ShelfController.Instance.Rows)
        {
            TapTicController.Instance.Failure();
            AudioController.Instance.EndLevelPlay(false);
            UIController.Instance.LevelEnd(false);
        }
    }

    private IEnumerator MoveToStartPos(float speed, Vector3 position)
    {
        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, speed);
            yield return null;
        }
        yield break;
    }

    private IEnumerator ChangeScale(float booster)
    {
        float time = 0.1f;
        Vector3 startScale = transform.localScale;

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.localScale = Vector3.Lerp(startScale, startScale * booster, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.localScale = Vector3.Lerp(startScale * booster, startScale, t);
            yield return null;
        }
        transform.localScale = startScale;
        yield break;
    }
}
