using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Hand : MonoBehaviour , IDropHandler, IPointerEnterHandler , IPointerExitHandler
{
    public GameObject BattleSystem;

    public void OnPointerEnter(PointerEventData eventData){
        //UnityEngine.Debug.Log ("Entered");
    }

    public void OnPointerExit(PointerEventData eventData){
        //UnityEngine.Debug.Log ("Exited");
    }

 public void OnDrop (PointerEventData eventData){
    UnityEngine.Debug.Log (eventData.pointerDrag.name + "was dropped onto" + gameObject.name);
    var input = eventData.pointerDrag.name;
    Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
    if (d != null)
    {
        d.parentToReturnTo = this.transform;
    }
    
    
 }
}
