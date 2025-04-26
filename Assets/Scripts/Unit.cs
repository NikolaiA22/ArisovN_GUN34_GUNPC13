using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public Cell Cell { get; set; }
    public event System.Action OnMoveEndCallback;

    [SerializeField] private float moveSpeed = 5f;

    private bool isMoving;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cell?.OnPointerEnter(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cell?.OnPointerClick(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cell?.OnPointerExit(eventData);
    }

    public void Move(Cell targetCell)
    {
        if (isMoving) return;

        isMoving = true;
        StartCoroutine(MoveCoroutine(targetCell));
    }

    private System.Collections.IEnumerator MoveCoroutine(Cell targetCell)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = targetCell.transform.position + Vector3.up * 0.5f;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        while (transform.position != endPos)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
            yield return null;
        }

        if (Cell != null) Cell.Unit = null;
        Cell = targetCell;
        Cell.Unit = this;
        isMoving = false;
        OnMoveEndCallback?.Invoke();
    }
}