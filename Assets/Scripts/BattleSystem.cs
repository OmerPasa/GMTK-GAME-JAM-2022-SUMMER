using System.Collections;
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

	private Animator animator;
    private string currentAnimaton;
	const string PLAYER_IDLE = "Player_Idle";
    const string PLAYER_DODGEBACKWARD = "Player_DodgeBackward";
    const string PLAYER_DODGEFORWARD = "Player_DodgeForward";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump_Gun";
    const string PLAYER_ATTACK = "player_attack";
    const string PLAYER_AIR_ATTACK = "Player_Jump_Firing";
    const string PLAYER_DEATH = "Player_Death";
    const string PLAYER_TAKEDAMAGE = "Player_TakeDamage";
	private bool isAttacking;
    private bool isTakingDamage;
    private bool isDying;
    private bool isntDead;

    void Start()
    {
		//ChangeAnimationState(PLAYER_IDLE);
		AudioSource animationsounds = GameObject.Find("animationsounds").GetComponent<AudioSource>();

		isntDead = true;
		state = BattleState.START;
		StartCoroutine(SetupBattle());
		animator = GetComponent<Animator>();
    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

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
		ChangeAnimationState(PLAYER_ATTACK);
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

	    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
