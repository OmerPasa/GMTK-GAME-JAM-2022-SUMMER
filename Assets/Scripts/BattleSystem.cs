﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	public GameObject DropzoneObject;
	public GameObject Hand;
	public List<GameObject> Cards = new List<GameObject>();

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;


	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

	public AudioSource animationsounds;
    public AudioClip[] hasarsoundArray;
	public AudioClip dealcard;

// NEEDS İMPLEMENTATİONS!!!!!!

    public GameObject Player;

    const string PLAYER_IDLE = "Player_Idle";

	private bool isAttacking;
    private bool isTakingDamage;
    private bool isDying;
    private bool isntDead;

    void Start()
    {
		AudioSource animationsounds = GameObject.Find("animationsounds").GetComponent<AudioSource>();

		isntDead = true;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();
		Player.GetComponent<Player>().ChangeAnimationState(PLAYER_IDLE);

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);


		yield return new WaitForSeconds(2f);
		
		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	public IEnumerator PlayerAttack(int BaseDamage)
	{
		bool isDead = enemyUnit.TakeDamage(BaseDamage);
		Debug.Log (BaseDamage + " amount of damage is given");
		enemyHUD.SetHP(enemyUnit.currentHP);
		animationsounds.clip = hasarsoundArray[Random.Range(0,hasarsoundArray.Length)];
		animationsounds.Play();

		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(1f);
		Debug.Log("waited");
		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		Player.GetComponent<Player>().ChangeAnimationState(PLAYER_IDLE);
		animationsounds.clip = hasarsoundArray[Random.Range(0,hasarsoundArray.Length)];
		animationsounds.Play();
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			DropzoneObject.SetActive (true);
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal(int hp)
	{
		playerUnit.Heal(hp);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton(int Dam)
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack(Dam));
	}

	public void OnHealButton(int hp)
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal(hp));
	}

	public void OnDrawCard()
	{
		animationsounds.clip = dealcard;
		animationsounds.Play();
		GameObject randCard = Cards[Random.Range(0, Cards.Count)];
		Transform parent = Hand.transform;

		if (Hand.transform.childCount < 6)
		{
			randCard.transform.SetParent(parent, false);
		}
	}


}
