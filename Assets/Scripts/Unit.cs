using UnityEngine;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Collider))]
public class Unit : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public event Action<Unit> OnMoveEndCallback;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Visual Feedback")]
    [SerializeField] private GameObject selectionVisual;
    [SerializeField] private Material highlightMaterial;

    private Cell _currentCell;
    private bool _isMoving = false;
    private Vector3 _targetPosition;
    private Material _originalMaterial;
    private Renderer _renderer;

    public Cell Cell
    {
        get => _currentCell;
        set
        {
            if (_currentCell != null)
                _currentCell.Unit = null;

            _currentCell = value;

            if (_currentCell != null)
                _currentCell.Unit = this;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ApplyHighlight(true);
        Cell?.OnPointerEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ApplyHighlight(false);
        Cell?.OnPointerExit(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Cell?.OnPointerClick(eventData);
    }
    private void ApplyHighlight(bool state)
    {
        if (_renderer == null) return;

        _renderer.material = state && highlightMaterial != null
            ? highlightMaterial
            : _originalMaterial;

        if (selectionVisual != null)
            selectionVisual.SetActive(state);
    }


    public void Move(Cell targetCell)
    {
        if (_isMoving || targetCell == null || targetCell == Cell)
            return;

        if (targetCell.Unit != null)
        {
            Debug.LogWarning($"Target cell {targetCell.name} is occupied!");
            return;
        }

        _isMoving = true;
        _targetPosition = targetCell.transform.position;

        var previousCell = Cell;
        Cell = null;

        StartCoroutine(MoveCoroutine(previousCell, targetCell));
    }

    private System.Collections.IEnumerator MoveCoroutine(Cell fromCell, Cell toCell)
    {
        Vector3 startPosition = transform.position;
        float journeyLength = Vector3.Distance(startPosition, _targetPosition);
        float startTime = Time.time;

        while (transform.position != _targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            transform.position = Vector3.Lerp(startPosition, _targetPosition, fractionOfJourney);
            yield return null;
        }

        transform.position = _targetPosition;
        Cell = toCell;
        _isMoving = false;

        OnMoveEndCallback?.Invoke(this);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform)
            {
                OnPointerClick(null);
            }
        }
    }
    private void OnMouseEnter() => Debug.Log("Mouse ENTER unit");
    private void OnMouseExit() => Debug.Log("Mouse EXIT unit");

    private void OnDestroy()
    {
        if (Cell != null)
            Cell.Unit = null;
    }
}
