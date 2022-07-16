using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler , IDragHandler , IEndDragHandler
{

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("on beginin of drag");
        var mouse = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");

        transform.position = Input.mousePosition;
    }
        public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("on the end of drag");

    }

}
