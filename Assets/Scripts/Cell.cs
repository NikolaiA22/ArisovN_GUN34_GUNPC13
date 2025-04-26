using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public GameObject focus;
    public GameObject select;
    public Dictionary<NeighbourType, Cell> Neighbours { get; set; }
    public Unit Unit { get; set; }

    public event System.Action<Cell> OnPointerClickEvent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (focus != null)
            focus.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (focus != null)
            focus.SetActive(false);
    }

    public void SetSelect(Material material)
    {
        select.SetActive(true);
        select.GetComponent<MeshRenderer>().material = material;
    }

    public void ResetSelect()
    {
        select.SetActive(false);
    }
}
