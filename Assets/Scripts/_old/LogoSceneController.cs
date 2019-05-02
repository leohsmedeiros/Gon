using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoSceneController : MonoBehaviour {

	public Image logo;

	int stepOfAnim = 0;

	float cronometer = 0;
	float timeToWait = 1.5f;

	Color newColor;

	// Use this for initialization
	void Start () {
		newColor = logo.color;
		newColor.a = 0;

		logo.color = newColor;

		MenuSceneController.startSceneFromMenu = true;
	}
	
	// Update is called once per frame
	void Update () {
		switch (stepOfAnim) {
		case 0:
			newColor.a += Time.deltaTime * 0.5f;
			if(newColor.a > 1)
			{
				stepOfAnim = 1;
				newColor.a = 1;
			}

			logo.color = newColor;

			break;
		case 1:
			cronometer += Time.deltaTime;
			if(cronometer > timeToWait)
			{
				stepOfAnim = 2;
			}

			break;
		case 2:
			newColor.a -= Time.deltaTime * 0.5f;
			if(newColor.a < 0)
			{
				stepOfAnim = 3;
				newColor.a = 0;
			}
			
			logo.color = newColor;
			
			break;
		case 3:
			Application.LoadLevel("Scene Gameplay");
			break;
		}


	}
}
