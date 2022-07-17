using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler , IDragHandler , IEndDragHandler
{
    Vector3 card;
    Vector3 diff;
    LayoutElement BoolBoy;
    public Transform parentToReturnTo = null;
    public AudioClip cardselect;
    public AudioSource cardsounds;
    void Start() {
        BoolBoy = GetComponent<LayoutElement>();
        AudioSource cardsounds = GameObject.Find("cardsounds").GetComponent<AudioSource>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardsounds.clip= cardselect;
        cardsounds.Play();        
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent( this.transform.parent.parent);
        Debug.Log("on beginin of drag");

        var mouse = Input.mousePosition;
        card = transform.position;
        diff = card - mouse;
        BoolBoy.ignoreLayout = true;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
       // Debug.Log("on drag");

        transform.position = diff + Input.mousePosition;
    }
        public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent( parentToReturnTo);
        BoolBoy.ignoreLayout = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Debug.Log("on the end of drag");

    }

}
