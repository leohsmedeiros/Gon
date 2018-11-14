using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameplaySceneController : MonoBehaviour {

	public GameObject[] Buttons;

	public CharacterBehavior player;
	public GameObject PlayerDeadPrefab;
	public CameraBehavior cam;

	public GameObject UIGameplay;
	public GameObject UIPostGameplay;
	public Image imageBgPostGameplayUI;
	public GameObject UIPlacarFinal;
	public GameObject infoOfFinalBoard;

	public GameObject[] ButtonsToExtra;
	public RectTransform ButtonsExtraBg;
	bool buttonsExtraSet = false;
	float cronometerToShowExtraButtons = 0;
	bool hasExtraItens = false;

	public Text CoinsText;
	public Text ScoreText;

	public Text ScoreTextFinal;
	public Text ScoreTextFinalHighScore;


	public Image[] statusIcon;
	
	public float timeOfRainning;
	float cronometerRainning = 0;

	public float timeOfInvisibility;
	float cronometerInvisibility = 0;

	public float timeOfDoubleJump;
	float cronometerDoubleJump = 0;
	
	public float timeOfBuffedShoots;
	float cronometerBuffedShoots = 0;

	public GameObject[] PowerBarsFromGUI;

	public GameObject rainParticle;
	public GameObject rainParticleEffectInPlayer;
	public GameObject healParticle;
	public GameObject[] lifesFromGUI;
	public GameObject invisibityParticle;
	public SpriteRenderer[] SpritesPlayer;
	public GameObject magicPowerParticle;
	public GameObject attackBuffParticle;
	public GameObject[] SpritesGUIAttack;
	public GameObject doubleJumpParticle;


	bool isInvisible = false;
	bool isRaining = false;
	[HideInInspector]
	public bool isAttackBuffed = false;
	[HideInInspector]
	public bool isDoubleJump = false;

	public GameObject menuAbility;
	bool MenuAbilityIsOpened = false;

	// 0 to 3
	public int numberOfLifes = 1;

	// 0 to 3
	public int numberOfPowerBars = 3;

	bool BatPowerMagic = false;

	bool died = false;

	[HideInInspector]
	public int score = 0;

	[HideInInspector]
	public int lifeBonusEnemy = 0;

	IEnumerator stopBatPower()
	{
		yield return new WaitForEndOfFrame ();
		BatPowerMagic = false;
	}

	public bool BatPowerMagicWasUsed()
	{
		return BatPowerMagic;
	}

	void OnEnable()
	{
		for (int i=0; i<Buttons.Length; i++) {
			Buttons[i].SetActive(true);
		}
	}

	void OnDisable()
	{
		for (int i=0; i<Buttons.Length; i++) {
			Buttons[i].SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		UpdateGUIHearts ();
		UpdateGUIPowerBars ();

		hasExtraItens = CheckIfHasExtraItems ();

//		Rect newrect = ButtonsExtraBg.rect;
//		newrect.width = 0;
//		ButtonsExtraBg.rect.width = newrect;
	}

	public void AddCoin(int qtd)
	{
		PlayerPrefs.SetInt("COINS", PlayerPrefs.GetInt("COINS") + qtd);

	}

	void UpdateGUIHearts()
	{
		for (int i=0; i<lifesFromGUI.Length; i++) {
			lifesFromGUI [i].SetActive (i < numberOfLifes);
		}
	}
	
	void UpdateGUIPowerBars()
	{
		for (int i=0; i<PowerBarsFromGUI.Length; i++) {
			PowerBarsFromGUI [i].SetActive (i < numberOfPowerBars);
		}
	}

	public void red_crystal_button(int qtdLifes)
	{
		numberOfLifes += qtdLifes;
		
		if (numberOfLifes > lifesFromGUI.Length)
			numberOfLifes = lifesFromGUI.Length;

		if (qtdLifes == 1) {
			ButtonsToExtra [0].gameObject.SetActive (false);
			PlayerPrefs.SetInt ("LOW_RED_CRYSTAL", PlayerPrefs.GetInt ("LOW_RED_CRYSTAL") - 1);
		} else {
			ButtonsToExtra [1].gameObject.SetActive (false);
			PlayerPrefs.SetInt ("HIGH_RED_CRYSTAL", PlayerPrefs.GetInt ("HIGH_RED_CRYSTAL") - 1);
		}

		UpdateGUIHearts ();
	}
	
	public void blue_crystal_button(int qtdMana)
	{
		numberOfPowerBars += qtdMana;
		
		if (numberOfPowerBars > PowerBarsFromGUI.Length)
			numberOfPowerBars = PowerBarsFromGUI.Length;

		if (qtdMana == 1) {
			ButtonsToExtra [2].gameObject.SetActive (false);
			PlayerPrefs.SetInt("LOW_BLUE_CRYSTAL", PlayerPrefs.GetInt("LOW_BLUE_CRYSTAL") - 1);
		} else {
			ButtonsToExtra [3].gameObject.SetActive (false);
			PlayerPrefs.SetInt("HIGH_BLUE_CRYSTAL", PlayerPrefs.GetInt("HIGH_BLUE_CRYSTAL") - 1);
		}

		UpdateGUIPowerBars ();
	}

	void ResetStatus()
	{
		isRaining = isInvisible = isAttackBuffed = isDoubleJump = false;
	}

	IEnumerator rainEffectCoroutine()
	{
		yield return new WaitForSeconds (1);
		rainParticleEffectInPlayer.SetActive (false);
		rainParticle.SetActive (true);
	}

	void SetRain(bool value)
	{
		isRaining = value;
		statusIcon [0].gameObject.SetActive(value);
		rainParticleEffectInPlayer.SetActive (value);

		if (value)
			StartCoroutine (rainEffectCoroutine ());
		else
			rainParticle.SetActive (false);
	}

	void SetInvisibility(bool value)
	{
		isInvisible = value;

		statusIcon [1].gameObject.SetActive(value);

		Color newColor = SpritesPlayer[0].color;
		
		if (value) {
			newColor.a = 0.4f;
			player.gameObject.tag = "Player-Invisible";
		} else {
			newColor.a = 1f;
			player.gameObject.tag = "Player";
		}
		
		invisibityParticle.SetActive (false);
		invisibityParticle.SetActive (true);
		
		foreach (SpriteRenderer sprite in SpritesPlayer) {
			sprite.color = newColor;
		}
	}
	
	void SetBuffedAttack(bool value)
	{
		statusIcon [2].gameObject.SetActive(value);

		isAttackBuffed = value;

		if (value) {
			attackBuffParticle.SetActive (false);
			attackBuffParticle.SetActive (true);
		}
	}

	
	void SetDoubleJump(bool value)
	{
		statusIcon [3].gameObject.SetActive(value);
		
		isDoubleJump = value;
		
		if (value) {
			doubleJumpParticle.SetActive (false);
			doubleJumpParticle.SetActive (true);
		}
	}

	bool CanUsePower()
	{
		if (isRaining || isInvisible || isAttackBuffed || isDoubleJump)
			return false;

		numberOfPowerBars -= 1;
		if(numberOfPowerBars < 0)
		{
			numberOfPowerBars = 0;
			return false;
		}

		UpdateGUIPowerBars();
		return true;
	}

	public void RainButton()
	{
		if (!CanUsePower ())
			return;

		SetRain (true);
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}
		
	public void HealButton()
	{
		if (!CanUsePower ())
			return;

		healParticle.SetActive (false);
		healParticle.SetActive (true);

		if (numberOfLifes < 3)
			numberOfLifes ++;

		UpdateGUIHearts ();
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}

	public void InvisibilityButton()
	{
		if (!CanUsePower () || isInvisible)
			return;
		
		SetInvisibility (true);

		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}
	
	public void MagicPowerButton()
	{
		if (!CanUsePower ())
			return;
		
		magicPowerParticle.SetActive (false);
		magicPowerParticle.SetActive (true);
		BatPowerMagic = true;
		StartCoroutine (stopBatPower ());
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}

	public void AttackBuffButton()
	{
		if (!CanUsePower ())
			return;

		SetBuffedAttack (true);
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}
	
	public void DoubleJumpButton()
	{
		if (!CanUsePower ())
			return;
		
		SetDoubleJump (true);
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}

	public void MenuAbilityButton()
	{
		MenuAbilityIsOpened = !MenuAbilityIsOpened;
	}

	public void looseLife()
	{
		looseLife (1);
	}

	public void looseLife(int qtdLifes)
	{
		numberOfLifes -= qtdLifes;
		UpdateGUIHearts ();
		if (numberOfLifes <= 0)
			Die ();
	}

	void SetButtonsExtra(bool value)
	{
		for (int i=0; i< ButtonsToExtra.Length; i++) {
			ButtonsToExtra[i].gameObject.SetActive(value);
		}
	}
	
	void SetButtonsExtra()
	{
		ButtonsToExtra[0].gameObject.SetActive( PlayerPrefs.GetInt("LOW_RED_CRYSTAL") > 0 );
		ButtonsToExtra[1].gameObject.SetActive( PlayerPrefs.GetInt("HIGH_RED_CRYSTAL") > 0 );
		ButtonsToExtra[2].gameObject.SetActive( PlayerPrefs.GetInt("LOW_BLUE_CRYSTAL") > 0 );
		ButtonsToExtra[3].gameObject.SetActive( PlayerPrefs.GetInt("HIGH_BLUE_CRYSTAL") > 0 );
	}
	
	bool CheckIfHasExtraItems()
	{
		if (PlayerPrefs.GetInt ("LOW_RED_CRYSTAL") > 0)
			return true;
		if (PlayerPrefs.GetInt ("HIGH_RED_CRYSTAL") > 0)
			return true;
		if (PlayerPrefs.GetInt ("LOW_BLUE_CRYSTAL") > 0)
			return true;
		if (PlayerPrefs.GetInt ("HIGH_BLUE_CRYSTAL") > 0)
			return true;

		return false;
	}

	public void ButtonHome()
	{
		MenuSceneController.startSceneFromMenu = true;
		Application.LoadLevel (Application.loadedLevel);
	}

	public void ButtonPlayAgain()
	{
		MenuSceneController.startSceneFromMenu = false;
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public void ButtonShop()
	{
		MenuSceneController.startSceneFromMenu = true;
		Application.LoadLevel ("Scene Shop");
	}

	void Die()
	{
		GameObject go = Instantiate (PlayerDeadPrefab, player.transform.position + new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
		Destroy (player.gameObject);
		cam.target = go.transform;

		UIGameplay.SetActive(false);
		UIPostGameplay.SetActive(true);

		died = true;
	}

	int stepEndAnim = 0;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			PlayerPrefs.SetInt("LOW_BLUE_CRYSTAL",3);
			PlayerPrefs.SetInt("HIGH_BLUE_CRYSTAL",3);
			PlayerPrefs.SetInt("LOW_RED_CRYSTAL",3);
			PlayerPrefs.SetInt("HIGH_RED_CRYSTAL",3);
		}

		ScoreText.text = score.ToString ();

		if (died) {
			switch(stepEndAnim)
			{
			case 0:
				int hs = PlayerPrefs.GetInt("HIGHSCORE");
				if(score > hs){
					PlayerPrefs.SetInt("HIGHSCORE", score);
				}

				ScoreTextFinal.text = score.ToString();
				ScoreTextFinalHighScore.text = PlayerPrefs.GetInt("HIGHSCORE").ToString();

				stepEndAnim = 1;

				break;

			case 1:

				imageBgPostGameplayUI.fillAmount += 0.01f;
				if(imageBgPostGameplayUI.fillAmount >= 1)
					stepEndAnim = 2;
				break;

			case 2:
				UIPlacarFinal.SetActive(true);
				stepEndAnim = 3;
				break;

			case 3:
				UIPlacarFinal.transform.GetComponent<Image>().fillAmount += 0.05f;

				if(UIPlacarFinal.transform.GetComponent<Image>().fillAmount >= 1)
					stepEndAnim = 4;
				break;
			case 4:
				infoOfFinalBoard.SetActive(true);
				stepEndAnim = 5;
				break;
			}
		}

		menuAbility.SetActive (MenuAbilityIsOpened);

		CoinsText.text = "x" + PlayerPrefs.GetInt("COINS").ToString();

		if (hasExtraItens) {
			if (ButtonsExtraBg.localScale.x < 1) {
				Vector3 newScale = ButtonsExtraBg.localScale;
				newScale.x += Time.deltaTime;
				ButtonsExtraBg.localScale = newScale;
			} else {
				if (!buttonsExtraSet) {
					SetButtonsExtra ();
					buttonsExtraSet = true;
				}

				cronometerToShowExtraButtons += Time.deltaTime;
				if (cronometerToShowExtraButtons > 5) {
					ButtonsExtraBg.gameObject.SetActive (false);
				}
			}
		}


		if (isRaining) {
			GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");
			foreach(GameObject fire in fires)
			{
				fire.SetActive(false);
			}

			cronometerRainning += Time.deltaTime;
			if(cronometerRainning > timeOfRainning)
			{
				cronometerRainning = 0;
				SetRain (false);
			}
		}

		if (isInvisible) {
			cronometerInvisibility += Time.deltaTime;
			if(cronometerInvisibility > timeOfInvisibility)
			{
				cronometerInvisibility = 0;
				SetInvisibility(false);
			}
		}

		SpritesGUIAttack [0].SetActive (!isAttackBuffed);
		SpritesGUIAttack [1].SetActive (isAttackBuffed);


		if (isAttackBuffed) {
			cronometerBuffedShoots += Time.deltaTime;
			if(cronometerBuffedShoots > timeOfBuffedShoots)
			{
				cronometerBuffedShoots = 0;
				SetBuffedAttack(false);
				if(attackBuffParticle != null)
					attackBuffParticle.SetActive (false);
			}
		}

		if (isDoubleJump) {
			cronometerDoubleJump += Time.deltaTime;
			if(cronometerDoubleJump > timeOfDoubleJump)
			{
				cronometerDoubleJump = 0;
				SetDoubleJump(false);
			}
		}
	}
}
