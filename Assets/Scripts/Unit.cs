using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Cell currentCell;

    public delegate void MoveEndCallBack();
    public event MoveEndCallBack OnMoveEndCallback;

    public float moveSpeed = 5f;

    public void Move(Cell targetCell)
    {
        if (targetCell == null) return;

        currentCell = targetCell;
        StartCoroutine(MoveToCell(targetCell.transform.position));
    }

    private IEnumerator MoveToCell(Vector3 targetPosition)
    { 
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / moveSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        OnMoveEndCallback?.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentCell?.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentCell?.OnPointerExit(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentCell?.OnPointerClick(eventData);
    }

    public void SetCurrentCell(Cell cell)
    {
        currentCell = cell;
    }
}
