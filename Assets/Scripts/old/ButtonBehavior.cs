using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour {

	public enum Action	{ jump, shoot };

	public Action action;
	public TouchController touchController;
	public CharacterBehavior gon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(touchController.checkIfTouchObject(this.gameObject))
		{

			switch(action)
			{
			case Action.jump:
				gon.Jump ();
				break;
			case Action.shoot:
				Debug.Log("SHOOT");
				gon.ThrowArrow ();
				break;
			}

		}

	}
}
