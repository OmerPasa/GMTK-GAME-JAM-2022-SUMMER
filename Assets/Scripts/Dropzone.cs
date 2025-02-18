using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dropzone : MonoBehaviour , IDropHandler
{
    public GameObject BattleSystem;
    public GameObject Player;
    public GameObject Deck;
    public AudioSource cardsounds;
    public AudioClip Card1;
    public AudioClip Card2;
    public AudioClip Card3;
    public AudioClip Card4;
    public AudioClip Card5;
    public AudioClip Card6;

    const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_ATTACK = "player_attack";
    const string PLAYER_ATTACK2 = "player_attack2";


    private void Start() {
        AudioSource cardsounds = GameObject.Find("cardsounds").GetComponent<AudioSource>();
    }

 public void OnDrop (PointerEventData eventData){

    Player.GetComponent<Player>().ChangeAnimationState(PLAYER_IDLE);
    UnityEngine.Debug.Log (eventData.pointerDrag.name + "was dropped onto" + gameObject.name);
    var input = eventData.pointerDrag.name;
    Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
    if (d != null)
    {
        d.parentToReturnTo = Deck.transform;
    }
    
    
    if (input == "DamageSword")
    {
        Player.GetComponent<Player>().ChangeAnimationState(PLAYER_ATTACK);
        cardsounds.clip = Card1;
        cardsounds.Play();
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(5);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword2")
    {
        Player.GetComponent<Player>().ChangeAnimationState(PLAYER_ATTACK);        
        cardsounds.clip = Card2;
        cardsounds.Play();
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(10);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword3")
    {
        Player.GetComponent<Player>().ChangeAnimationState(PLAYER_ATTACK2);
        cardsounds.clip = Card3;
        cardsounds.Play();
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(15);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword4")
    {
        Player.GetComponent<Player>().ChangeAnimationState(PLAYER_ATTACK);
        cardsounds.clip = Card4;
        cardsounds.Play();
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(20);
        gameObject.SetActive (false);
    }
    else if (input == "DamageSword5")
    {
        Player.GetComponent<Player>().ChangeAnimationState(PLAYER_ATTACK);
        cardsounds.clip = Card5;
        cardsounds.Play();
        BattleSystem.GetComponent<BattleSystem>().OnAttackButton(25);
        gameObject.SetActive (false);
    }
    else if (input == "Meditation")
    {
        cardsounds.clip = Card6;
        BattleSystem.GetComponent<BattleSystem>().OnHealButton(Random.Range(1, 40));
        gameObject.SetActive (false);
    }
    else
    {
        gameObject.SetActive (false);
    }
 }
}
