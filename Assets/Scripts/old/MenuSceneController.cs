using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSceneController : MonoBehaviour {

	// scale (0.8f, 0.8f, 0.8f)
	public Transform stars;
	public Transform Placa;
	public GameObject luxCenario;
	public GameObject gameplay;

	static public bool startSceneFromMenu = true;

	public Transform horizontalMenu;
	public Transform referencePosition_Closed;
	public Transform referencePosition_Opened;

	public float speedAnimHorizontalMenu;
	int stateHorizontalMenu = 0;

	public GameObject credits;
	public GameObject tutorial;
	public Text textBoxTutorial;
	public GameObject[] imgButtonTutorialSelected;

	public Transform menuArrow;

	int step = 0;

	// Use this for initialization
	void Start () {
		if (!startSceneFromMenu)
			ButtonStartGame ();
	}

	
	public void ChangeStateMenuHorizontal()
	{
		Vector3 scale = new Vector3 (-1, 1, 1);

		stateHorizontalMenu++;


		if (stateHorizontalMenu > 1) {
			scale.x = 1;
			stateHorizontalMenu = 0;
		}

		menuArrow.localScale = scale;
	}

	public void ShowCredits(bool value)
	{
//		ShowTutorial (false);
		credits.SetActive (value);
	}
	
	public void ShowTutorial(bool value)
	{
//		ShowCredits (false);
		tutorial.SetActive (value);
	}

	
	public void Tutorial_button_Pressed(int indexButton)
	{
		for(int i = 0; i < imgButtonTutorialSelected.Length; i++)
		{
			imgButtonTutorialSelected[i].SetActive(false);
		}

		imgButtonTutorialSelected [indexButton].SetActive (true);
		switch (indexButton) {
		case 0:
			textBoxTutorial.text = "The blue bars shows the number of abilities you can use. The hearts is your life bar, when it downs to zero you die.";
			break;
		case 1:
			textBoxTutorial.text = "Represents the number of enemies that you have killed, or in another words, your score.";
			break;
		case 2:
			textBoxTutorial.text = "When you tap it will dropdown a menu with your abilities";
			break;
		case 3:
			textBoxTutorial.text = "Movement bar. You can use it to move Gon";
			break;
		case 4:
			textBoxTutorial.text = "Attack button. Tap it to shoot an arrow";
			break;
		}
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			ChangeStateMenuHorizontal();
		}

		Vector3 newPos;

		// PLACA
		switch(step)
		{
		case 0:
			newPos = Placa.position;
			newPos.y -= Time.deltaTime * 40;

			if(newPos.y <= 0)
			{
				newPos.y = 0;
				step = 1;
			}

			Placa.position = newPos;

			break;

		case 1:
			Vector3 newScale = stars.localScale;
			newScale += new Vector3(0.1f, 0.1f, 0.1f);

			if(newScale.x > 0.8f)
			{
				newScale = new Vector3(0.8f, 0.8f, 0.8f);
				step = 2;
				luxCenario.SetActive(true);
			}

			stars.localScale = newScale;

			break;		
		}

		if (step < 1)
			return;


		newPos = horizontalMenu.position;

		switch (stateHorizontalMenu) {
		// closed
		case 0:
			newPos.x += speedAnimHorizontalMenu*Time.deltaTime;

			if(newPos.x > referencePosition_Closed.position.x)			
				newPos.x = referencePosition_Closed.position.x;

			break;

		// opened
		case 1:
			newPos.x -= speedAnimHorizontalMenu*Time.deltaTime;

			if(newPos.x < referencePosition_Opened.position.x)
				newPos.x = referencePosition_Opened.position.x;
			
			break;
		}
		horizontalMenu.position = newPos;

	}


	public void ButtonStartGame()
	{
//		Application.LoadLevel ("Scene Gameplay");
		gameplay.SetActive (true);
		this.transform.parent.gameObject.SetActive (false);
	}
	
	public void ButtonShop()
	{
		Application.LoadLevel ("Scene Shop");
	}
}
