using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {
	
	public bool checkIfTouchObject(GameObject obj, TouchPhase typeOfTouch)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			if (Input.GetTouch (0).phase != typeOfTouch)
				return false;
		}
		
		return checkIfTouchObject (obj);
	}
	
	public bool checkIfTouchObject(GameObject obj)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			if(Input.touchCount == 0)
				return false;
		} else {				
			return ( Input.GetMouseButton(0) );
		}


		for (int i=0; i<Input.touchCount; i++) {

			Vector3 pos = getTouchPos_WorldPos (i);
			Vector2 touchPos = new Vector2 (pos.x, pos.y);
			Collider2D hit = Physics2D.OverlapPoint (touchPos);
		
			if (hit && hit.gameObject == obj) {
				return true;
			}

		}
		
		return false;
	}
	
	
	public Vector3 getTouchPos(int finger_index)
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
			return Input.GetTouch(finger_index).position;
		else
			return Input.mousePosition;
	}
	
	public Vector3 getTouchPos_WorldPos(int finger_index)
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (getTouchPos (finger_index));
		pos.z = 0;
		return pos;
	}
	
}
