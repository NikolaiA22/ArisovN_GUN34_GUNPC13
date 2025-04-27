using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class Unit : MonoBehaviour, IPointerClickHandler
{
    [Header("Visual Settings")]
    [SerializeField] private Material selectedMaterial;
    [SerializeField] private Material hoverMaterial;
    [SerializeField] private GameObject selectionIndicator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Renderer _renderer;
    private Material _originalMaterial;
    private Cell _currentCell;
    private bool _isSelected;
    private Coroutine _moveCoroutine;

    public Cell CurrentCell
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

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;

        if (selectionIndicator != null)
            selectionIndicator.SetActive(false);
    }

    private void Start()
    {
        FindAndAssignCell();
    }

    private void FindAndAssignCell()
    {
        if (CellManager.Instance == null || CellManager.Instance.AllCells == null)
        {
            Debug.LogWarning("CellManager not initialized yet");
            return;
        }

        Cell closestCell = null;
        float minDistance = float.MaxValue;

        foreach (var cell in CellManager.Instance.AllCells)
        {
            if (cell == null) continue;

            float distance = Vector3.Distance(transform.position, cell.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestCell = cell;
            }
        }

        if (closestCell != null && closestCell.Unit == null)
        {
            CurrentCell = closestCell;
            transform.position = closestCell.transform.position;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isSelected)
        {
            Deselect();
        }
        else
        {
            Select();
        }
    }

    public void Select()
    {
        if (_currentCell == null)
        {
            Debug.LogWarning("Cannot select - no cell assigned");
            return;
        }

        _isSelected = true;
        ApplySelectionVisual(true);
        CellHighlighter.Instance.HighlightMovementCells(_currentCell);
        UnitSelectionManager.Instance.SelectUnit(this);
    }

    public void Deselect()
    {
        _isSelected = false;
        ApplySelectionVisual(false);
        CellHighlighter.Instance.ResetHighlight();
    }

    public void MoveToCell(Cell targetCell)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MoveCoroutine(targetCell));
    }

    private IEnumerator MoveCoroutine(Cell targetCell)
    {
        if (targetCell == null || targetCell.Unit != null)
            yield break;

        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetCell.transform.position - startPosition);

        float distance = Vector3.Distance(startPosition, targetCell.transform.position);
        float duration = distance / moveSpeed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float progress = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, targetCell.transform.position, progress);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetCell.transform.position;
        transform.rotation = targetRotation;
        CurrentCell = targetCell;
        Deselect();
    }

    private void ApplySelectionVisual(bool state)
    {
        if (_renderer != null)
        {
            _renderer.material = state ? selectedMaterial : _originalMaterial;
        }

        if (selectionIndicator != null)
        {
            selectionIndicator.SetActive(state);
        }
    }

    private void OnMouseEnter()
    {
        if (!_isSelected && _renderer != null && hoverMaterial != null)
        {
            _renderer.material = hoverMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (!_isSelected && _renderer != null)
        {
            _renderer.material = _originalMaterial;
        }
    }

    private void OnDestroy()
    {
        if (_currentCell != null)
            _currentCell.Unit = null;
    }
}
