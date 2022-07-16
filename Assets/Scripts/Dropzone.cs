using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dropzone : MonoBehaviour , IDropHandler
{
    public GameObject BattleSystem;

 public void OnDrop (PointerEventData eventData){
    UnityEngine.Debug.Log (eventData.pointerDrag.name + "was dropped onto" + gameObject.name);
    var input = eventData.pointerDrag.name;
    Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
    if (d != null)
    {
        d.parentToReturnTo = this.transform;
    }
    
    
    if (input == "DamageSword")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(5);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword2")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(10);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword3")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(15);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword4")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(20);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword5")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(25);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword6")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(30);
        gameObject.SetActive (false);
    }
    else if (input == "Meditation")
    {
        //Player.GetDamage(5)
        BattleSystem.GetComponent<BattleSystem>().OnHealButton(Random.Range(1, 40));
        gameObject.SetActive (false);
    }
    else
    {
        gameObject.SetActive (false);
    }
 }
}
