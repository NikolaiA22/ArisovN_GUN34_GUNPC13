using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public GameObject selectMesh;
    public GameObject focusMesh;
    public Material originalMaterial;
    public Material highlightMaterial;

    public delegate void PointerClickEvent(Cell clickedCell);
    public event PointerClickEvent OnPointerClickEvent;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        ResetSelect();

        if (focusMesh != null)
        {
            focusMesh.SetActive(false);
        }
    }
    public Unit Unit { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Renderer>().material = highlightMaterial;
        focusMesh.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Renderer>().material = originalMaterial;
        focusMesh.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke(this);
    }

    public void SetSelect(Material selectMaterial)
    {
        selectMesh.SetActive(true);
        GetComponent<Renderer>().material = selectMaterial;
    }
    public void ResetSelect()
    {
        selectMesh.SetActive(false);
        GetComponent<Renderer>().material = originalMaterial;
    }
}
