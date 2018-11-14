using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopSceneController : MonoBehaviour {

	public Text TextCoin;
	public GameObject[] windows;

	public Text messageToUser;

	public int[] prices_virtual_money;

	public Text[] boardWithPrices;
	// Use this for initialization
	void Start () {
		for (int i=0; i<boardWithPrices.Length; i++) {
			boardWithPrices[i].text = prices_virtual_money[i].ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
		TextCoin.text = PlayerPrefs.GetInt ("COINS").ToString();

		if (Input.GetKeyDown (KeyCode.P)) {
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") + 20);
		}
	}

	public void PlusButton()
	{
		Debug.Log ("Entrou");
		windows[0].SetActive (false);
		windows[1].SetActive (true);
	}

	public void ReturnButton()
	{
		windows[0].SetActive (true);
		windows[1].SetActive (false);
	}
	
	public void CloseButton()
	{
		Application.LoadLevel("Scene Gameplay");
	}

	public void BuyItemWithVirtualMoney(int indexItem)
	{

		if (PlayerPrefs.GetInt ("COINS") > prices_virtual_money [indexItem]) {
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") - prices_virtual_money [indexItem]);
		} else {
			messageToUser.text = "Not enough coins";
			return;
		}

		switch (indexItem) {
		case 0:
			PlayerPrefs.SetInt ("LOW_RED_CRYSTAL", PlayerPrefs.GetInt ("LOW_RED_CRYSTAL") + 1);
			messageToUser.text = "+1 Low crystal red";
			break;
		case 1:
			PlayerPrefs.SetInt ("HIGH_RED_CRYSTAL", PlayerPrefs.GetInt ("HIGH_RED_CRYSTAL") + 1);
			messageToUser.text = "+1 High crystal red";
			break;
		case 2:
			PlayerPrefs.SetInt ("LOW_BLUE_CRYSTAL", PlayerPrefs.GetInt ("LOW_BLUE_CRYSTAL") + 1);
			messageToUser.text = "+1 Low crystal blue";
			break;
		case 3:
			PlayerPrefs.SetInt ("HIGH_BLUE_CRYSTAL", PlayerPrefs.GetInt ("HIGH_BLUE_CRYSTAL") + 1);
			messageToUser.text = "+1 High crystal blue";
			break;
		}
	}


	public void Success(sdkbox.Product product)
	{
		messageToUser.text = "Success!";


		switch (product.name) {
		case "1_packagecoins.":
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") + 20);
			break;
		case "2_packagecoins.":
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") + 40);
			break;
		case "3_packagecoins.":
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") + 80);
			break;
		case "4_packagecoins.":
			PlayerPrefs.SetInt ("COINS", PlayerPrefs.GetInt ("COINS") + 200);
			break;
		}
		
	}


	public void Fail(sdkbox.Product product, string erro)
	{
		messageToUser.text = "Fail! " + erro;

	}

}
