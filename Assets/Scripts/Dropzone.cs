using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dropzone : MonoBehaviour , IDropHandler, IPointerEnterHandler , IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData){
        UnityEngine.Debug.Log ("Entered");
    }

    public void OnPointerExit(PointerEventData eventData){
        UnityEngine.Debug.Log ("Exited");
    }

 public void OnDrop (PointerEventData eventData){
    UnityEngine.Debug.Log ("OnDrop to" + gameObject.name);
    Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
    if (d != null)
    {
        d.parentToReturnTo = this.transform;
    }
 }
}
