using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler , IDragHandler , IEndDragHandler
{
    Vector3 card;
    Vector3 diff;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("on beginin of drag");
        var mouse = Input.mousePosition;
        card = transform.position;
        diff = card - mouse;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("on drag");

        transform.position = diff + Input.mousePosition;
    }
        public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("on the end of drag");

    }

}
