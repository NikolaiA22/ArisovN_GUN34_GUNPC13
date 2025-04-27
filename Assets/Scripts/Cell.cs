using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    //[SerializeField]
    //private float _speed = 2f;
    //public Cell cell { get; set; }

    //public event Action OnMoveEndCallback;

    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.Space))
    //    { 
    //        //to do jump
    //    }

    //    var axis = Input.GetAxis("Horizontal");
    //    if (axis > 0f)
    //    { 

    //    }
    //}

    //public void Move(Cell cell)
    //{
    //    StartCoroutine(OnMove(cell));
    //}



    public GameObject selectMesh;
    public Material originalMaterial;
    public Material highlightMaterial;

    public delegate void PointerClickEvent(Cell clickedCell);
    public event PointerClickEvent OnPointerClickEvent;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
        ResetSelect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Renderer>().material = originalMaterial;
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
