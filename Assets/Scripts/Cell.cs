using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject highlightEffect;
    private Material _originalMaterial;
    private Renderer _renderer;

    public Unit Unit { get; set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _originalMaterial = _renderer.material;

        if (highlightEffect != null)
            highlightEffect.SetActive(false);
    }

    public void SetHighlight(Material highlightMaterial)
    {
        if (_renderer != null && highlightMaterial != null)
        {
            _renderer.material = highlightMaterial;
        }

        if (highlightEffect != null)
        {
            highlightEffect.SetActive(true);
        }
    }

    public void ResetHighlight()
    {
        if (_renderer != null)
        {
            _renderer.material = _originalMaterial;
        }

        if (highlightEffect != null)
        {
            highlightEffect.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (UnitSelectionManager.Instance.SelectedUnit != null)
        {
            UnitSelectionManager.Instance.MoveSelectedUnit(this);
        }
    }
}
